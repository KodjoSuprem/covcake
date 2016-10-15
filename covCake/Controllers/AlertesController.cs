using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using covCake.DataAccess;
using covCake.Models;

namespace covCake.Controllers
{

    public class AlertesController : BaseController
    {
        [RedirectAuthorize]
        [PageTitle("Vos alertes email")]
        public ActionResult Index(int? index)
        {
            //Affichage de toutes les alertes
            //possibilité de supprimer et éditer
            int idx = index ?? 0;
            AlerteViewData viewData = new AlerteViewData();

            viewData.Alertes = CurrentUser.Alertes.ToPagedList(idx);
            return View(viewData);
        }


        [RedirectAuthorize]
        public ActionResult Edit(int alertId)
        {
            IAlerte alert = Data.AlerteDataAccess.GetAlert(alertId);
            AlerteViewData alertVD = new AlerteViewData();
            alertVD.Load(alert);
            return View(alertVD);
        }


        [RedirectAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteAll(string[] alertIds)
        {

            try
            {
                alertIds = alertIds.Where(s => s != "false").ToArray();
                List<IAlerte> alertToDelete = new List<IAlerte>();

                foreach (string alertId in alertIds)
                    alertToDelete.Add(Data.AlerteDataAccess.GetAlert(int.Parse(alertId)));


                IEnumerable<IAlerte> alertesFiltered = alertToDelete.Where(a => a.UserId == CurrentUser.UserId);
                Data.AlerteDataAccess.DeleteAlertes(alertesFiltered);

            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        [RedirectAuthorize]
        [PageTitle("Créer une nouvelle alerte email")]
        public ActionResult Create()
        {
            //Editer et Créer une alerte
            return View();
        }


        [RedirectAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(AlerteViewData alerteViewData)
        {
            //Editer et Créer une alerte

            int? paysId = alerteViewData.paysArrive;
            DateTime? dateDebut = alerteViewData.dateDebut;
            //DateTime? dateFin = newAlerte.dateFin;
            int moisFin = alerteViewData.moisFin ?? 0;
            int moisDebut = alerteViewData.moisDebut ??0;
            int? nbJours = alerteViewData.nbJours;// ?? 0;
            DateTime? periodeFin = null, periodeDeb = null ;
            IPays pays = null;
            string villeArrive = (alerteViewData.villeArrive.Trim().IsNullOrEmpty())? null : alerteViewData.villeArrive.Trim();

            if (moisDebut > 0 && moisFin < 1)
            {
                this.ModelState.AddModelError("moisFin", "Vous devez spécifier le mois de fin de période");

            }
            if (moisDebut > 0 && moisFin > 0)
            {
                int year = DateTime.Today.Year;

                periodeDeb = new DateTime(year, moisDebut, 1);
                if (moisFin < moisDebut) year++; // si le mois de fin avant mois de début sa avance a l'année d'apres
                periodeFin = new DateTime(year, moisFin++, 1); //au 1er du mois d'apres
            }
            

            if(paysId != null)
            {
                pays = Data.PaysDataAccess.GetPays(paysId.Value);
                if( pays == null)
                    this.ModelState.AddModelError("paysArrive", "Le pays spécifié n'a pas été trouvé."); 
            }

            if (dateDebut == null && nbJours < 1)
               this.ModelState.AddModelError("dateDebut", "Vous devez spécifier une date de départ ou un nombre de jour");


            if (this.ModelState.IsValid)
            {
                //Insertion de l'alerte
                //TODO: Mettre l'insertion de l'alerte en fonction??

                IAlerte newAlert = Data.AlerteDataAccess.Create();
                newAlert.IdPays = paysId;
                newAlert.NbJours = nbJours;
                newAlert.UserId = this.CurrentUserId;
                newAlert.PeriodeDebut = periodeDeb;
                newAlert.PeriodeFin = periodeFin;
                newAlert.DateDebutProjet = dateDebut;
                newAlert.VilleArrive = villeArrive;

                newAlert.DateDernierEnvoi = this.CurrentUser.AccountCreationDate;//initialisation à la date de création du compte
                newAlert.DateCreation = DateTime.Now; 

                Data.AlerteDataAccess.InsertAlerte(newAlert);
                return RedirectToAction("CreateSuccess"); //View("CreateSuccess");
            }

            return View(alerteViewData);

          
        }
        [PageTitle("Créer une nouvelle alerte email")]
        public ActionResult CreateSuccess()
        {
            return View();
        }


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">Mot de passe </param>
        /// <returns></returns>
        public ActionResult SendAll(string p)
        {
            if (this.CheckAlertSenderPassword(p) == false)
            {
                this.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                ViewData["pass"] = p;
                return View("BadPassword");
            }

            bool UPDATEALERTSTAMP = true;
#if DEBUG //---------DEBUG-------------
            UPDATEALERTSTAMP = false;
#endif    //---------------------------
            IEnumerable<Services.ProjetAlertsRetreiver.ProjectAlertsResults> rez;
            rez = Services.ProjetAlertsRetreiver.RetrieveProjectsByUsers(UPDATEALERTSTAMP);

           foreach (Services.ProjetAlertsRetreiver.ProjectAlertsResults item in rez)
           {
               if (item.projets.Count() > 0)
               {
                   //TODO: Loggage (nb d'alertes, detinataire etc..)
                   this.CovCakeMailer.SendProjetAlertMail(item.user, item.projets, item.nbAlerts);       
               }
           }

            //Enregistre les updates d'alerte 
           CovCake.DataProvider.AlerteDataAccess.SubmitChanges();

            //OUF....
           return Content("Alerts sent to " + rez.Count() + " users");
        }



        private IAlerte InsertNewAlert(IAlerte alert)
        {
            throw new NotImplementedException();
        }

        private bool CheckAlertSenderPassword(string pass)
        {
            return (string.Compare(pass, CovCakeConfiguration.AlertSenderPassword) == 0);
        }

    }
}

