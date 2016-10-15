using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using covCake.DataAccess;
using covCake.Models;

using System.Data.Linq;
using System.Transactions;
using covCake.Services;
using System.Web.Mvc.Html;

namespace covCake.Controllers
{


    public class ProjetsController : BaseController
    {

        private const int NB_PROJETS_PAR_PAGE_LISTE = 6;


        /// <summary>
        /// JAJAX
        /// </summary>
        /// <returns></returns>
        public ActionResult Expand()
        {
            string s = "alert('enculer')";

            return JavaScript(s);
        }


        /// <summary>
        /// AjaxOnly
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [Authorize]
        public ActionResult GetRelatedProjets(SearchProjetParams searchParams, int? page )
        {
            //TODO: Doit utiliser le meme systeme de recherche ke pour les listes

            const int offsetDaysDate = 20;
            const int offsetDaysJours = 15;

            IQueryable<IProjet> completeList;

            completeList = Data.ProjetDataAccess.GetAllProjets();
            int? paysId = searchParams.paysArriveId;
            DateTime? dateDepart = searchParams.dateDebut;
            int nbJours = searchParams.nbJours ?? 0;
            page = (page >= 0) ? page : 0;

            if (paysId != null)
                completeList = completeList.ProjetsTo(paysId.Value).Where(p => p.OwnerUserId != this.CurrentUserId);

            if (dateDepart != null)
            {   //TODO: verifier que le projet n'est pas complet ou deja réalisé 
                completeList = completeList
                    .ProjetsBetween(dateDepart.Value.AddDays(-offsetDaysDate), dateDepart.Value.AddDays(offsetDaysDate));
                //.Where(p => p.PaysArriveEntity.LibellePays.ToLower().Contains(query))

            }
            else if (nbJours > 0)
            {
                //Inclus les projets incertains
                completeList = completeList//Data.ProjetDataAccess.GetAllProjets()
                    //.Where(p => p.PaysArrive.LibellePays.ToLower().Contains(query)
                     .Where(p => nbJours - offsetDaysJours <= p.NbJours && p.NbJours <= nbJours + offsetDaysJours);
            }
   
            ListeProjetViewData relatedProjViewData = new ListeProjetViewData()
            { 
                IsSearchResults = true,
                ListeProjets = completeList.ToPagedList(page.Value, NB_PROJETS_PAR_PAGE_LISTE),
                SearchParams = searchParams
            };

            return PartialView("RelatedProjetsList", relatedProjViewData );
        }

        /// <summary>
        /// Affiche un projet en particulier
        /// </summary>
        /// <param name="ProjetId"></param>
        /// <returns></returns>
        [GetViewInfos]
        [GetModelError]
        public ActionResult Index(int? projetId)
        {
            IProjet proj;
            if (projetId == null)
                return RedirectToRoute(CovCake.Routes.PROJETLIST);

                proj = Data.ProjetDataAccess.GetProjet(projetId.Value);
                if (proj == null)
                {
                    string listUrl = this.Url.RouteUrl(CovCake.Routes.PROJETLIST);
                    string listLink = "<a href='"+listUrl+"'>liste</a>";
                    string searchLink = "<a href='"+listUrl+"'>recherche</a>";

                    var errmsg = "Le voyage n°{0} n'existe pas ou plus...<br/> Consultez la {1} ou faites une nouvelle {2}.".FormatWith(projetId, listLink, searchLink);
                    return Error(errmsg);
                    //return View("Error", error);
                }
                proj.Visites++;
                Data.ProjetDataAccess.SubmitChanges();
                this.SetPageTitle(proj.GetFriendDisplayName()); // proj.GetLongDisplayName();

                string textSearch = proj.PaysArrive.LibelleEngPays;// +" " + proj.PaysArrive.CapitalePaysEng;
                FlickrPhotos pictures = FlickrServices.SearchPhotos(textSearch).Photos;
                pictures = pictures ?? FlickrServices.GetDefaultPhotos(textSearch);

                IndexProjetViewData projetViewData = new IndexProjetViewData();
                projetViewData.Projet = proj;
                projetViewData.FlickrPictures = pictures;
       
            return View(projetViewData);

        }

        [RedirectAuthorize()]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// View
        /// </summary>
        /// <param name="createValues"></param>

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(int projetId)
        {
      
            IProjet proj = Data.ProjetDataAccess.GetProjet(projetId);
            if(proj == null)
            {
                return RedirectToAction("MonCompte", "User");
            }
   
            string /*transPrix = "0", resPrix = "0", resMaxHote = "0",*/ resType = "", modeTrans = "";
            double? transPrix = null,  resPrix = null;
            int? resMaxHote =null;
            DateTime? dateDeb, dateFi;
            dateDeb = proj.DateDebut;
            dateFi = proj.DateFin;// != null) ? proj.DateFin.Value.ToShortDateString() : "";

            if(proj.Residence != null)
            {
                resPrix = proj.Residence.Prix;
                if(proj.Residence.MaxHotes > 1) resMaxHote = proj.Residence.MaxHotes;
                resType = proj.Residence.Type;
            }


            if(proj.Transports.Count() > 0)
            {
                ITransport trans = proj.Transports.First();
                modeTrans = trans.ModeTransport;
                transPrix = trans.PrixAR; ;
            }
            int? nbjours = proj.NbJours;
            CreateProjetViewData projetViewData = new CreateProjetViewData()
            {
                adresse = (proj.Residence != null) ? proj.Residence.Adresse : "",
                commentaires = proj.Commentaires,
                dateDebut = dateDeb,
                dateFin = dateFi,
                nbjours = (proj.NbJours > 0) ? nbjours : null,
                villeArrive = proj.VilleArrive,
                incertain = bool.Parse(proj.Incertain.ToString().ToLower()),
                modetrans = modeTrans,
                prixtrans= transPrix,
                prixres = resPrix,//(prresPrix > 0) ? proj.NbJours.ToString() : "",,
                typeres = resType ,
                maxhotes = resMaxHote,

                Projet = proj
            };

            ViewData["title"] = "Editer mon voyage vers " + proj.PaysArrive.LibellePays;
            return View(projetViewData);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(CreateProjetViewData newDataProjet)
        {
           
            /*
             newDataProjet = new CreateProjetViewData()
            {
                nbjours = "45",
                incertain = "true",
                details = "fhdzhenbdk",
               // typeres = "HOTEL",
                typeres = "FAKE",
                prixres = "45,76",
              
                modetrans = "FAKE",
                //modetrans = "TRAIN",
                adresse = "",
                prixtrans = "5",
                commentaires = "sale nbatar",
                projetid = "15"

            }; //
            */
            IProjet proj = UpdateProjet(newDataProjet);

            try
            {
                Data.ProjetDataAccess.SubmitChanges();
                SendProjetEditNotification(proj);
            }
            catch(ChangeConflictException lnkEx)
            {

            }
            catch(Exception ex)
            {
                ViewData["ErrorMsg"] = "Votre projet de voyage n'a pas pu être modifié."; //"Une erreur est survenue lors de l'envoi de l'email de confirmation. Si le probleme persiste veillez contacter " + Configuration.SiteAdminEmail + ".";
                return View("Error");
            }
            
            return RedirectToRoute(CovCake.Routes.MONCOMPTE);//."MonCompte","User");// View("CreateSuccess");

        }


        private IProjet UpdateProjet(CreateProjetViewData newDataProjet)
        {

            // DateTime datedeb, datefin;



            DateTime? vDateDeb = null, vDateFin = null;

            int maxhotes = newDataProjet.maxhotes ?? -1;
            double prixres = 0d, prixtrans = 0d;
            bool incertain;
            int nbJours = -1;
          //  newDataProjet.incertain = (!string.IsNullOrEmpty(newDataProjet.incertain)) ? newDataProjet.incertain : bool.FalseString;
            incertain = newDataProjet.incertain;

            if(newDataProjet.projetid == null)
                throw new Exception("Le projet à éditer est introuvable");

            int projetId = newDataProjet.projetid.Value;
            IProjet proj = Data.ProjetDataAccess.GetProjet(projetId);
            if (proj == null)
                throw new Exception("Le projet à éditer est introuvable");

            if(proj.OwnerUserId != this.CurrentUserId)
                throw new Exception(this.CurrentUserId.ToString() + " n'est pas le propriétaire de ce projet et ne peut pas le modifier");


            IResidence res = null;
            ITransport trans = null;
            if(GotResidence(newDataProjet.typeres))
                res = Data.ResidenceDataAccess.CreateResidence();

            if(GotTransport(newDataProjet.modetrans))
                trans = Data.TransportDataAccess.CreateTransport();

            //using(TransactionScope transx = new TransactionScope())
            //{
            proj.DateModification = DateTime.Now;

            proj.Incertain = incertain;
            if(!incertain)
            {
                vDateDeb = newDataProjet.dateDebut;
                vDateFin = newDataProjet.dateFin;

                proj.DateDebut = vDateDeb;
                proj.DateFin = vDateFin;
                proj.NbJours = (int)proj.GetDuree().timeSpan.TotalDays;
            }
            else
            {
                //   nbJours = 
                proj.DateDebut = null;
                proj.DateFin = null;
                proj.NbJours = newDataProjet.nbjours.Value;
            }


            proj.Commentaires = newDataProjet.commentaires.Trim();


            //   proj.OwnerUserId = this.CurrentUser.UserId;

            //remove residence
            if(res == null)
            {
                if(proj.Residence != null)
                {
                   // int resId = proj.Residence.Id;
                   //  proj.Residence = null;
                   //  Data.CovCakeDataContext.SubmitChanges();

                   //  Data.ProjetDataAccess.SubmitChanges();
                   //  Data.ResidenceDataAccess.Remove(resId);
                    Data.ResidenceDataAccess.Remove(proj.Residence.Id);
                }
            }
            else
            {
                res.Prix = newDataProjet.prixres ?? 0;//double.Parse(residenceData.prixres.Trim());
                res.Type = newDataProjet.typeres.Trim().ToUpper();
                res.Adresse = newDataProjet.adresse.Trim();
                if(res.Type == "CHEZ MOI" || res.Type == "AMIS")
                    res.MaxHotes = maxhotes; 
                proj.Residence = res;
            }

            if(trans == null)
            {
                //  Data.TransportDataAccess.Remove(proj.Transports.FirstOrDefault().IdTransport);
                proj.TransportsEntitySet.Clear();
            }
            else
            {
                proj.TransportsEntitySet.Clear();

                trans.ModeTransport = newDataProjet.modetrans.Trim().ToUpper();

                trans.Details = newDataProjet.details ?? "";
                trans.PrixAR = newDataProjet.prixtrans ?? 0;//prixtrans;

                if(trans.ModeTransport == "AVION") // || trans.ModeTransport.Trim() == "
                {
                    trans.Compagnie = newDataProjet.compagnie;//?? null;
                    trans.NumVol = newDataProjet.numvol;//?? null;
                }

                proj.TransportsEntitySet.Add((cov_Transport)trans);

            }
          //  Data.ProjetDataAccess.SubmitChanges();

            //  transx.Complete();
            return proj;
            // }
        }

        public ActionResult Search(SearchProjetParams searchArgs, int? page)
        {
            const int offsetDaysDate = 10;
            const int offsetDaysJours = 5;

            try
            {
                this.ModelState.Clear();
                page = page ?? 0;
                page = (page >= 0) ? page : 0;

                searchArgs.paysArrive = searchArgs.paysArrive.Trim();
                this.SetPageTitle("Voyages vers " + searchArgs.paysArrive);

                string query = searchArgs.paysArrive;
                if (string.IsNullOrEmpty(query))
                    searchArgs.paysArrive = "~Tous~";
                else if (searchArgs.nbJours != null && (searchArgs.nbJours < 1 || searchArgs.nbJours > 365))
                    this.ModelState.AddModelError("_FORM", "Le nombre de jour doit être compris entre 1 et 365");


                //   searchArgs.paysArrive = searchArgs.paysArrive.ToTitleCase();
                IQueryable<IProjet> completeList;

                //TODO: Faire une recherche en enlevant les accents
                IPays pays = Data.PaysDataAccess.GetPaysFr(query);
                // pays = Data.PaysDataAccess.GetPaysFrNoAccent(query);
                if (!string.IsNullOrEmpty(query) && pays == null)
                {
                    ModelState.AddModelError("_FORM", "Aucun voyages ne concerne ce pays");
                    //TempData["noresult"] = true;
                    //return RedirectToAction("Liste");
                }
                if (ModelState.IsValid)
                {

                    int nbJours = searchArgs.nbJours ?? 0;
                    //TODO: pas comprendre sa...
                    DateTime? dateDepart = searchArgs.dateDebut;//(searchArgs.dateDebut.HasValue) ? searchArgs.dateDebut.Value.AddDays(nbJours) as DateTime? : null;

                    // IQueryable< IPays> pays = Data.PaysDataAccess.GetAllPays().Where(p => p.LibellePays.ToLower().Contains(query));

                    completeList = Data.ProjetDataAccess.GetAllProjets();
                    if (pays != null)
                        completeList = completeList.ProjetsTo(pays.IdPays);

                    if (dateDepart != null)
                    {   //TODO: verifier que le projet n'est pas complet ou deja réalisé 
                        completeList = completeList
                            .ProjetsDateDebutBetween(dateDepart.Value.AddDays(-offsetDaysDate), dateDepart.Value.AddDays(offsetDaysDate));
                        //.Where(p => p.PaysArriveEntity.LibellePays.ToLower().Contains(query))

                    }
                    if (nbJours > 0)
                    {
                        //Inclus les projets incertains
                        completeList = completeList//Data.ProjetDataAccess.GetAllProjets()
                            //.Where(p => p.PaysArrive.LibellePays.ToLower().Contains(query)
                             .Where(p => nbJours - offsetDaysJours <= p.NbJours && p.NbJours <= nbJours + offsetDaysJours);

                    }

                    completeList = completeList.OrderBy(p => p.DateDebut);
                    //IQueryable<IProjet> nonOutdated = Data.ProjetDataAccess.GetAllProjets().ProjetsNonOutdated().OrderBy(p => p.DateDebut);
                    //if(nonOutdated.Count() > 5)
                    if (completeList.Count() == 0)
                    {
                        //Afficher tout ls voyage si aucune réponse na été trouvée
                        completeList = ProjetBasicList();
                        ViewData["noresult"] = true;
                        TempData["noresult"] = true;
                        //return RedirectToAction("Liste");
                    }

                    ListeProjetViewData projets = new ListeProjetViewData()
                    {
                        ListeProjets = completeList.ToPagedList(page.Value, NB_PROJETS_PAR_PAGE_LISTE),
                        IsSearchResults = true,
                        SearchParams = searchArgs
                    };


                

                    return View("Liste", projets);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("_FORM", "Une erreur est apparue lors de votre recherche"); 
            }
           

            ListeProjetViewData basicListeProjets = new ListeProjetViewData()
            {
                ListeProjets = ProjetBasicList().ToPagedList( page.Value, NB_PROJETS_PAR_PAGE_LISTE),
                IsSearchResults = true,
                SubstituteListe = true,
                SearchParams = searchArgs
            };
            return View("Liste", basicListeProjets);//(ListeProjetsViewData)ViewData.Model);
        }

        [RedirectAuthorize()]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(ProjetViewData projetData, ResidenceViewData residenceData, TransportViewData transportData)
        {
            
            /*
             projetData = new ProjetViewData
            {
                commentaires = "Projet qui me tien à coeur",
                villeArrive = "Paris",
                dateDebut = "22/08/09",
                dateFin = "25/12/09",
                heureDepart = "22h32",
                paysArrive = "25",
                paysDepart = "77"
            };
             residenceData = new ResidenceViewData
            {
                adresse = "14 rue des longchamps 91650 breux",
                maxhotes = "23",
                prixres = "32.54",
                typeres = "chez moi"
            };

             transportData = new TransportViewData
            {
                numvol = "B5464",
                modetrans = "Avion",
                prixtrans = "43.54",
                details = null
            };

            */
            DateTime datedeb, datefin;
            DateTime? vDateDeb = null, vDateFin = null;
            int paysarr, paysdep;
            int maxhotes = -1;
            double prixres = 0d, prixtrans = 0d;
            bool incertain;
            int nbJours = -1;
            projetData.incertain = (!string.IsNullOrEmpty(projetData.incertain)) ? projetData.incertain : bool.FalseString;
            incertain = bool.Parse(projetData.incertain);
            if(!incertain)
            {
                if(DateTime.TryParse(projetData.dateDebut, out datedeb) == false)
                    this.ModelState.AddModelError("dateDebut", "La date de départ est incorecte");
                else if(datedeb.Year >= DateTime.Now.Year + 2)
                    this.ModelState.AddModelError("dateDebut", "La date de départ doit se situer dans les 2 prochaines années");
                else if(datedeb <= DateTime.Now)
                    this.ModelState.AddModelError("dateDebut", "La date de départ souhaitée est déjà dépassée");

                if(DateTime.TryParse(projetData.dateFin, out datefin) == false)
                    this.ModelState.AddModelError("dateFin", "La date de retour prevue est incorecte");
                else if(datefin.Year >= DateTime.Now.Year + 2)
                    this.ModelState.AddModelError("dateFin", "La date de retour doit se situer dans les 2 prochaines années");
                else if(datefin <= DateTime.Now)
                    this.ModelState.AddModelError("dateFin", "La date de retour est déjà dépassée");
                else if(datefin < datedeb.AddDays(1))
                    this.ModelState.AddModelError("dateFin", "La date de retour doit être au moins 1 jour apres la date de départ");

                vDateDeb = datedeb;
                vDateFin = datefin;
            }
            else
            {
                if(!int.TryParse(projetData.nbjours, out nbJours))
                {
                    this.ModelState.AddModelError("nbjours", "La duree du voyage est incorrecte");
                }
                else if(nbJours < 1)
                {
                    this.ModelState.AddModelError("nbjours", "Le voyage doit durer au minimum 1 jour");
                }
                else if(nbJours >= (365 * 2))
                {
                    this.ModelState.AddModelError("nbjours", "Le voyage doit durer maximum 2 ans ");
                }

            }

            if(int.TryParse(projetData.paysArrive, out paysarr) == false)
                this.ModelState.AddModelError("paysArrive", "Le pays de destination selectionné est incorecte");

            if(int.TryParse(projetData.paysDepart, out paysdep) == false)
                this.ModelState.AddModelError("paysDepart", "Le pays de départ selectionné est incorecte");

            if(incertain/*bool.Parse(projetData.incertain) */&& projetData.dateDebut.Trim() != "" && projetData.dateFin.Trim() != "" && int.TryParse(projetData.nbjours, out nbJours))
            {
                this.ModelState.AddModelError("nbjours", "Vous devez spécifier une durée en nombre de jour si vous ne pouvez indiquer de dates précises");
            }

            if(projetData.dateDebut.Trim() != "" && projetData.dateFin.Trim() != "" && incertain == true && !int.TryParse(projetData.nbjours, out nbJours))
            {
                this.ModelState.AddModelError("_FORM", "Vous devez spécifier une durée en nombre de jour ou des dates précises");
            }

            //Convertion des "." en virgule
            //FIXME: CREERPROJET Plante si plusieurs "." ou "," dans le prixres trans
            residenceData.prixres = residenceData.prixres.Replace(".", ",").Trim();
            transportData.prixtrans = transportData.prixtrans.Replace(".", ",").Trim();



            if(residenceData.prixres != "" && double.TryParse(residenceData.prixres, out prixres) == false)
                this.ModelState.AddModelError("prixres", "Le prix indiqué est incorrecte");

            if(transportData.prixtrans != "" && double.TryParse(transportData.prixtrans, out prixtrans) == false)
                this.ModelState.AddModelError("prixtrans", "Le prix indiqué est incorrecte");


            if(residenceData.typeres.ToUpper() == "CHEZ MOI" || residenceData.typeres.ToUpper() == "AMIS")
            {
                residenceData.maxhotes = residenceData.maxhotes.Trim();
                if(residenceData.maxhotes != "" && int.TryParse(residenceData.maxhotes, out maxhotes) == false)
                    this.ModelState.AddModelError("maxhotes", "Nombre d'hotes maximum incorrecte");
            }



            if(this.ModelState.IsValid)
            {
                IProjet proj = Data.ProjetDataAccess.Create();
                IResidence res = Data.ResidenceDataAccess.CreateResidence();
                ITransport trans = Data.TransportDataAccess.CreateTransport();

                proj.DateCreation = DateTime.Now;
                proj.DateModification = proj.DateCreation;
                proj.Incertain = incertain;
                if(!incertain)
                {
                    proj.DateDebut = vDateDeb;
                    proj.DateFin = vDateFin;
                    proj.NbJours = (int)proj.GetDuree().timeSpan.TotalDays;
                }
                else
                {
                    proj.DateDebut = null;
                    proj.DateFin = null;
                    proj.NbJours = nbJours;
                }

                proj.IdPaysDepart = paysdep;
                proj.IdPaysArrive = paysarr;
                proj.Commentaires = projetData.commentaires.Trim();
                proj.OwnerUserId = this.CurrentUser.UserId;



                proj.VilleArrive = projetData.villeArrive.Trim().ToTitleCase();



                if(GotResidence(residenceData.typeres))
                {

                    res.Prix = prixres;//double.Parse(residenceData.prixres.Trim());
                    res.Type = residenceData.typeres.Trim().ToUpper();
                    res.Adresse = residenceData.adresse.Trim();


                    if(res.Type == "CHEZ MOI" || res.Type == "AMIS")
                        res.MaxHotes = maxhotes;
                    proj.Residence = res;

                }
                if(GotTransport(transportData.modetrans))
                {
                    trans.ModeTransport = transportData.modetrans.Trim().ToUpper();

                    //TODO: ajpouter details du transport dans la vue. Modale??
                    trans.Details = transportData.details ?? "";
                    trans.PrixAR = prixtrans;

                    if(trans.ModeTransport == "AVION")
                    {
                        trans.Compagnie = transportData.compagnie;//?? null;
                        trans.NumVol = transportData.numvol;//?? null;
                    }
                    proj.TransportsEntitySet.Add((cov_Transport)trans);

                }
               

                try
                {
                    Data.ProjetDataAccess.InsertProjet(proj);
                }
                catch(Exception ex)
                {

                    string errmsg = "Votre projet de voyage n'a pas pu être enregistré."; 
                    //"Une erreur est survenue lors de l'envoi de l'email de confirmation. Si le probleme persiste veillez contacter " + Configuration.SiteAdminEmail + ".";
                   
                    //TODO:REDIRECT TO ACTION
                    return Error(errmsg);
                    
                }

                return RedirectToAction("CreateSuccess", new { IdProjet = proj.IdProjet });

            }
            return View();
        }

        public ActionResult CreateSuccess(int IdProjet)
        {
           
            IProjet proj = null;
            try
            {
                 proj = Data.ProjetDataAccess.GetProjet((IdProjet));

            }
            catch (Exception)
            {
                return View("Error");
            }
            return View("CreateSuccess", proj);

        }

        private bool GotResidence(string typeresidence)
        {
            return cov_Residence.TYPESRESIDENCE.Contains(typeresidence.ToUpper());
        }

        private bool GotTransport(string modetransport)
        {
            return cov_Transport.MODETRANSPORT.Contains(modetransport.ToUpper());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjetId"></param>
        /// <returns></returns>
        [Authorize]
        
        public ActionResult Delete(int projetId)
        {
            try
            {
                IProjet proj = Data.ProjetDataAccess.GetProjet(projetId);
                if(CurrentUserId == proj.OwnerUserId)
                {
                    Data.ProjetDataAccess.Remove(proj);

                    TempData["ViewInfos"] = new string[] { "Le voyage " + proj.GetShortDisplayName() + " a bien été supprimé" };
                }

            }
            catch(Exception ex)
            {
                Dictionary<string, string> modelError = new Dictionary<string, string>();
                modelError.Add("_FORM", "Le voyage n°" + projetId + " n'a pas pu être supprimé");
                TempData["ModelError"] = modelError;
            }

        
            return RedirectToAction("MonCompte", "User");

        }


        private IQueryable<IProjet> ProjetBasicList()
        {
            //TODO: Incertain puis Ordre croissant date de début??? inverse? mélangé??
            //pour linstant incertain, croissant
            return Data.ProjetDataAccess.GetAllProjets().OrderBy(p => p.DateDebut);
        }

       // [OutputCache(Duration=10,VaryByParam="page")]
        //TODO: si trops de requettes remettre le cache.
        [PageTitle("Voyages")]
        public ActionResult Liste(int? page)
        {
            page = page ?? 0;
            //TODO : Return uniquement les projets dont la dateDepart est pas dépassée
            IQueryable<IProjet> completeList = ProjetBasicList();
            //IQueryable<IProjet> nonOutdated = Data.ProjetDataAccess.GetAllProjets().ProjetsNonOutdated().OrderBy(p => p.DateDebut);
            //if(nonOutdated.Count() > 5)

            PagedList<IProjet> projList = new PagedList<IProjet>(completeList, page.Value, NB_PROJETS_PAR_PAGE_LISTE);
            if (projList.Count() < 1)
            {
                string errorMsg = "La page {0} n'existe pas.".FormatWith(page);
           
                string redirectUrl = Url.RouteUrl(CovCake.Routes.PROJETLIST, new { page=0});
                return ErrorRedirect(errorMsg, 8, redirectUrl);
            }
               
            ListeProjetViewData projets = new ListeProjetViewData()
            {
                ListeProjets = projList
            };

            if(TempData.ContainsKey("noresult"))
                ViewData["noresult"] = TempData["noresult"];

            return View(projets);
        }

        [RedirectAuthorize]
        public ActionResult Subscribe(int projetId)
        {

            IAbonnementProjet newAbo = MakeSubscribtion(CurrentUser.UserId, projetId);
            int pId = projetId;
            int notifyDelayMinutes = CovCakeConfiguration.SubscriptionNotificationDelay / 60;
            if (newAbo != null)
            {
                //Envoi de mail différé, de XX secondes. XX == CovCakeConfiguration.SubscriptionNotificationDelay
                this.SendSubscribtionNotification(newAbo);
                 string[] infoMessage = 
                     { 
                        "Vous avez maintenant rejoint le voyage " + newAbo.Projet.GetShortDisplayName() , 
                        "Un email de notification sera envoyé à " + newAbo.Projet.OwnerUserProfile.DisplayName + " dans " + notifyDelayMinutes + " minutes si vous participez toujours au voyage "
                     };
                 TempData["ViewInfos"] = infoMessage;
                return RedirectToAction("Index", new { projetId = pId });
            }
            else
            {
                this.ModelState.AddModelError("_FORM","L'inscription à ce voyage à échoué");
                TempData[GetModelErrorAttribute.MODEL_STATE_DICTIONARY_DEFAULT_KEY] = this.ModelState;
                return RedirectToAction("Index", new { projetId = pId });
            }
            // Data.s

        }

        [RedirectAuthorize]
        public ActionResult UnSubscribe(int projetId)
        {
            //IProjet proj = Data.ProjetDataAccess.GetProjet(projetId);
            IAbonnementProjet abo = Data.AbonnementProjetDataAccess.GetAbonnement(this.CurrentUserId,projetId);
           
            if (abo == null)
            {
                ViewData["ErrorMsg"] = "Le voyage concerné n'éxiste pas, impossible d'effectuer la desinscription";
                return View("Error");
            }

            //int pId = projetId;
            //Envoi de mail différé de 
     
            bool succeed = MakeUnSubscribtion(abo);
            if (succeed)
            {
                this.SendProjetResignNotification(abo);
                TempData["ViewInfos"] = "Vous avez quitter le voyage " + abo.Projet.GetShortDisplayName();
                return RedirectToAction("Index", new { projetId = abo.ProjetId });
            }
            else
            {
                this.ModelState.AddModelError("_FORM", "La désinscription au voyage " + abo.Projet.GetShortDisplayName() + " à échoué ");
                TempData[GetModelErrorAttribute.MODEL_STATE_DICTIONARY_DEFAULT_KEY] = this.ModelState;
                return RedirectToAction("Index", new { projetId = abo.ProjetId });
            }

        }

      
        /// <summary>
        /// Réalise la désinscription
        /// </summary>
        /// <param name="abo"></param>
        /// <returns></returns>
        private bool MakeUnSubscribtion(IAbonnementProjet abo)// Guid UserId ,int projetId)
        {
            try
            {
                Data.AbonnementProjetDataAccess.DeleteAbonnement(abo);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
            //IProjet proj = Data.ProjetDataAccess.GetProjet(projetId);

            //IAbonnementProjet abo = Data.AbonnementProjetDataAccess.GetAbonnement(UserId, projetId);
        }

        /// <summary>
        /// Réalise l'inscription
        /// </summary>
        /// <param name="projetId"></param>
        /// <returns></returns>
        private IAbonnementProjet MakeSubscribtion(Guid UserId, int projetId)
        {
            //Si le mec est déja inscrit
            //au cas ou il refresh ou force en entrant l'url 
            if (Data.AbonnementProjetDataAccess.GetAbonnement(UserId, projetId) != null)
                return null;
            IAbonnementProjet abo = Data.AbonnementProjetDataAccess.CreateAbonnement();
            abo.DateAbonnement = DateTime.Now;
            abo.UserId = UserId;
            abo.ProjetId = projetId;

            IProjet proj = Data.ProjetDataAccess.GetProjet(projetId);
            try
            {
                using(TransactionScope trans = new TransactionScope())
                {
                    proj.UserAbonnesEntitySet.Add((cov_AbonnementProjet)abo);
                    Data.ProjetDataAccess.SubmitChanges();
                    trans.Complete();
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return abo;
        }


        //Send Email notification !!!

        #region Send email notififcation

        private void SendProjetEditNotification(IProjet proj)
        {
            CovCakeMailer.SendProjetEditNotify(proj);
        }

        private void SendSubscribtionNotification(IAbonnementProjet newAbo)
        {
            int delayNotif = CovCakeConfiguration.SubscriptionNotificationDelay;
            //TODO: a tester dans un contexte de charge importante (meme si yaura pas 10000 subscribtion en meme temps)
            //delayNotif = 10;
            DelayedTaskExecuter.ExecuteLaterAsync(delayNotif, (obj, timedOut) =>
            {
                IAbonnementProjet abo = obj as IAbonnementProjet;
                //Apres le delay on verifie si le mec est toujour bien inscrit et on envoi le mail
                lock (obj)
                {
                    if (abo.UserProfile.ParticipeA(abo.ProjetId))
                    {
                        try
                        {

                            CovCakeMailer.SendProjectSubscribtionNotify(abo);
                            CovCakeMailer.SendFriendsProjectSubscribNotify(abo);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

            }, newAbo);
        }

        private void SendProjetResignNotification(IAbonnementProjet abo)
        {
            CovCakeMailer.SendProjectResignNotify(abo);
            /*
            int delayNotif = 30;
            DelayedTaskExecuter.ExecuteLaterAsync(delayNotif, (obj, timedOut) =>
            {
                //Apres le delay on verifie si le mec est toujour bien inscrit et on envoi le mail
                lock (obj)
                {
                        try
                        {
                            CovCakeMailer.SendProjectResignNotify(abo);
                        }
                        catch (Exception)
                        {
                        }      
                }

            }, abo);
             * */
        }



        #endregion
    }
}
