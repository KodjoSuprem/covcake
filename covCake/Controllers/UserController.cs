using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using covCake.DataAccess;
using covCake.Models;
using covCake.Services;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;


namespace covCake.Controllers
{

    public class UserController : BaseController
    {
        private const int USERPROJETS_LISTESIZE = 3;

     
        private ActionResult GetUserPage(string UserNameOrId)
        {
            ErrorRedirectViewData UserError = new ErrorRedirectViewData
            {
                DelaySeconds = 5,
                RedirectUrl = Url.Action("Index", "Home"),// "/Projets/Liste",
                ErrorMsg = "Cet utilisateur n'existe pas."
            };

            try
            {
                IUserProfile user = Data.UserDataAccess.GetUser(new Guid(UserNameOrId));

                if(user == null)
                {
                    user = Data.UserDataAccess.GetUser(UserNameOrId);
                }
                if(user == null)
                {
                    return View("ErrorRedirect", UserError);
                }
                SetPageTitle("Profil de " + user.UserName);
                return View(user);
            }
            catch(Exception ex)
            {

                return View("ErrorRedirect", UserError);
            }
        }

        //[OutputCache(Duration=300, VaryByParam="userId")]
        
        
        /// <summary>
        /// Profil d'un utilisateur
        /// </summary>
        /// <param name="userId">userId shrinké</param>
        /// <returns></returns>
        public ActionResult Index(string userId)
        {
            string NO_USER_ERRORMESSAGE = "Cet utilisateur n'existe pas.";
            Guid userGuid = userId.ToGuid();

            if (userId.IsNullOrEmpty())
                return HomeIndex();
            
            if (userGuid.IsEmpty())
                return Error(NO_USER_ERRORMESSAGE);
            
  
            /*
            //Le current user ne peux voir sa propre fiche et tombe sur MonCompte
            if (userId.Value == CurrentUserId)
                return RedirectToAction("MonCompte");
        

            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < st.FrameCount; i++)
            {
                System.Diagnostics.StackFrame sf = st.GetFrame(i);
                sb.AppendLine(
                        " Line: " + sf.GetFileLineNumber() +
                        " Method: " + sf.GetMethod() + "<br/>");
            }

            CovCake.Log.Mail.Info(sb.ToString());
        
            */
 
            IUserProfile user = Data.UserDataAccess.GetUser(userGuid);
            if(user == null)
            {
                /*
                ErrorRedirectViewData UserError = new ErrorRedirectViewData
                {
                    DelaySeconds = 5,
                    RedirectUrl = Url.Action("Index", "Home"),// "/Projets/Liste",
                    ErrorMsg = NO_USER_ERRORMESSAGE
                };
                */
                //ViewData["ErrorMsg"] = "Cet utilisateur n'existe pas.";
                //return View("Error");
                return Error(NO_USER_ERRORMESSAGE);
                //return View("ErrorRedirect", UserError);
            }

            this.SetPageTitle("Profil de " + user.DisplayName);
            return View(user);
        }

        /// <summary>
        ///  Non utilisé changement d'image par EditInfos
        /// </summary>
        /// <returns></returns>
        [RedirectAuthorize]
        public ActionResult ChangePhoto()
        {
            //this field is never empty, it contains the selected filename
            //if(string.IsNullOrEmpty(userImage))
            //{
            //    ModelState.AddModelError("FileBlob", "Please upload a file");
            //}
            ViewData["imageChanged"] = false;
            var file = Request.Files["userImage"];
            if (file == null)
            {
                ModelState.AddModelError("userImage", "Please upload a file");
            }
            else if (file.ContentType != "" || Path.GetExtension(file.FileName) == "")
            {
                ModelState.AddModelError("userImage", "Le format du fichier est invalide.");
            }
            if (ModelState.IsValid)
            {
                IUserProfile currentUser = this.CurrentUser;
                string fullImageFolder = CovCakeConfiguration.UserPhotoStorageFolder;
                string newFilename = currentUser.UserId.ToString() + Path.GetExtension(file.FileName);
                string serverSideFileName = Path.Combine(fullImageFolder, newFilename);
                currentUser.ImagePersoPath = serverSideFileName;
                //insertion en base
                Data.UserDataAccess.InsertUser(currentUser);
                //sauvegarde du fichier
                file.SaveAs(currentUser.ImagePersoPath);
                ViewData["imageChanged"] = true;

                return View();
            }

            return View();
        }

        [RedirectAuthorize]
        [PageTitle("Mon Compte")]
        [GetViewInfos]
        public ActionResult MonCompte(int? page, int? idxPageProjets,int? idxPageAbonnes, int? idxPageSub)
        {

            if(CurrentUser == null)
            {
                string errMsg = "Impossible de charger les informations de votre compte.";
         
                return Error(errMsg);
            }
            MonCompteViewData monCompteViewData = new MonCompteViewData(Data);
            monCompteViewData.UserProfile = CurrentUser;

            if(TempData.ContainsKey("ViewInfos"))
            {
                ViewData["ViewInfos"] = TempData["ViewInfos"];
            }

            if(TempData.ContainsKey("ModelError"))
            {
                foreach (KeyValuePair<string,string> item in TempData["ModelError"] as Dictionary<string,string>)
	            {
                    ModelState.AddModelError(item.Key, item.Value);
	            }
            }

            IQueryable<IProjet> userProjets = Data.ProjetDataAccess.GetAllProjetOf(this.CurrentUser.UserId);

            //Si ajax pas besoin des news ~_~
            if(!Request.IsAjaxRequest())
            {

                //7 jours avant la derniere visite
                int offsetDays = 5; // x derniers jours avant la derniere connexion
                DateTime lastNewsDate = this.CurrentUser.LastActivityDate.AddDays(-offsetDays);
                //tt les messages non lu depuis la date de derniere connexion moins x jours


                //Messages récents
                IQueryable<IMessagePrive> newsNewMessages = this.CurrentUser.MessagesRecus.GetAllMessagesSince(lastNewsDate).GetAllReceiverUnreadedMessages();  //GetAllUnreadedMessages();

                //Abonnement récents
                IQueryable<IUserProfile> newsAbonnes = Data.AbonnementProjetDataAccess.GetAllAbonnementsConcerning(userProjets).GetAllAbonnementSince(lastNewsDate).GetAllUserProfiles();  
                //this.CurrentUser.AbonnementProjets.GetAllAbonnementSince(lastNewsDate).Select(A => A.UserProfile);

                IQueryable<IProjet> newsSimilarProjets = Data.ProjetDataAccess.GetAllProjets().SameDestinationAs(userProjets).Except(userProjets).ProjetsCreeDepuis(lastNewsDate);

                // newsSimilarProjets = Data.ProjetDataAccess.SameDestinationAs(this.CurrentUser.Projets);
                //Covoyageurs récents
                IQueryable<IAbonnementProjet> newsMates = Data.AbonnementProjetDataAccess.GetAllSharedAbonnementSimple(this.CurrentUser.UserId).GetAllAbonnementSince(lastNewsDate);

                monCompteViewData.NewsAbonnes = newsAbonnes;
                monCompteViewData.NewsMates = newsMates;
                monCompteViewData.NewsNewMessages = newsNewMessages;
                monCompteViewData.NewsSimilarProjets = newsSimilarProjets;
                monCompteViewData.NewsDaysOffset = offsetDays;
                monCompteViewData.LastNewsDate = lastNewsDate;
            }

            page = page ?? 0;
            int idxAbonnes = idxPageAbonnes ?? page.Value;
            int idxSub = idxPageSub ?? page.Value;
            int idxProjets = idxPageProjets ?? page.Value;

           // int USERPROJETS_LISTESIZE = 3;
            monCompteViewData.UserProjets = userProjets.OrderByDescending(p => p.DateModification).ToPagedList(idxProjets, USERPROJETS_LISTESIZE);
            monCompteViewData.UserAbonnements = CurrentUser.AbonnementProjets.ToPagedList(idxSub);
            //monCompteViewData.UserAbonnes = CurrentUser.Projets.GetAllAbonnes.GetAllUserProfiles().ToPagedList(idxProjets);
           
           
            //var g = newsAbonnes.this.UserManager.ToString();
            if(Request.IsAjaxRequest())
            {
                return PartialView("UserProjetsList", monCompteViewData);
            }

            return View(monCompteViewData);
        }
        
        //AJAX only
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UserProjetsList(int? page)
        {
          //  if(!Request.IsAjaxRequest())

            if(CurrentUser == null)
            {
                //return View("Error");
            }
            
            MonCompteViewData monCompteViewData = new MonCompteViewData(Data);

            IQueryable<IProjet> userProjets = Data.ProjetDataAccess.GetAllProjetOf(this.CurrentUser.UserId);
            int idxLstProj = page??0;
            monCompteViewData.UserProjets = userProjets.OrderByDescending(p => p.DateModification).ToPagedList(idxLstProj, USERPROJETS_LISTESIZE);

            return PartialView("UserProjetsList", monCompteViewData);
 
        }

        //AJAX only
        /// <summary>
        /// Liste des abonnement du Current User
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UserAbonnementsList(int? page)
        {
            MonCompteViewData monCompteViewData = new MonCompteViewData(Data);
            int idxLstSubsc = page ?? 0;
            monCompteViewData.UserAbonnements = CurrentUser.AbonnementProjets.ToPagedList(idxLstSubsc);

            return PartialView("UserAbonnementsList", monCompteViewData);
        }

        /// <summary>
        /// Liste des abonnes pour les projets du CurrentUser
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UserAbonnesList(int? page)
        {
            MonCompteViewData monCompteViewData = new MonCompteViewData(Data);
            int idxLstSubsc = page ?? 0;

            //monCompteViewData.UserAbonnes.AddRange(CurrentUser.Projets.GetAllAbonnes());

            monCompteViewData.UserProfile = CurrentUser;
            monCompteViewData.UserProjets = CurrentUser.Projets.ToPagedList(page ?? 0);

            return PartialView("UserAbonnesList", monCompteViewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SendPrivateMessage(string message)
        {
            if(string.IsNullOrEmpty(message.Trim()))
            {
                ModelState.AddModelError("message", "Le message à envoyer est vide.");
            }
            return View();
        }
       

        /// <summary>
        /// Prépapre la vu pour le formulaire denvoi de messages à plusieurs utilisateurs
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SendMessages()
        {
            int SubscriberPageSize = 8;
            //PagedList<IUserProfile> subscriberList = null;// new PagedList<IUserProfile>(
            ////PagedList<IAbonnementProjet> aboList = new PagedList<IAbonnementProjet>(;
            //List<IAbonnementProjet> aboList = new List<IAbonnementProjet>();
          
            //foreach (var proj in  CurrentUser.Projets)
            //{
            //    IQueryable<IUserProfile> users = Data.AbonnementProjetDataAccess.GetAllAbonnements(proj.IdProjet).Select( abo => abo.UserProfile);
            //   // charger pr page Projet 1  only

            //}

            SendMessageViewData messageData = new SendMessageViewData();
            messageData.Subscribers = new List<IUserProfile>();
            IProjet firstProjet = CurrentUser.Projets.First();
            if(firstProjet != null)
            {
                messageData.Subscribers = Data.AbonnementProjetDataAccess.GetAllAbonnements(firstProjet.IdProjet).Select(abo => abo.UserProfile).ToList();
            }

            return View(messageData);
        }

        /// <summary>
        /// AJAX ONLY
        /// </summary>
        /// <returns></returns>
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangeUserDesc(string element_id,string original_html , string update_value)
        {
            IUserProfile currUser = this.CurrentUser;
            if(currUser != null){
                if(currUser.Description != update_value)
                {
                    currUser.Description =  update_value.HtmlEncode();
                    Data.UserDataAccess.Save();
                }
            }
            return Content(update_value.HtmlEncode());
        }

        [RedirectAuthorize]
        [PageTitle("Editer mes informations")]
        public ActionResult EditInfos()
        {
            IUserProfile currUser = this.CurrentUser;
            if (currUser == null)
            {
                this.ForceUserLogOut();
                return LoginPage();
            }
            UserInfosViewData userInfos = new UserInfosViewData(currUser);
            return View(userInfos);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [PageTitle("Editer mes informations")]
        public ActionResult EditInfos(UserInfosViewData newUserInfos)
        {
            /*
             if(String.IsNullOrEmpty(newUserInfos.email))
             {
                 ModelState.AddModelError("email", "Vous devez enter une adresse email.");
             }
             else if(!CovCakeMailer.IsValidEmail(newUserInfos.email))
             {
                 ModelState.AddModelError("email", "Cette adresse email est invalide.");
             }
            */
             if(string.IsNullOrEmpty(newUserInfos.nom))
             {
                 ModelState.AddModelError("nom", "Vous devez entrer un nom, celui ci ne sera jamais affiché dans son entier (ex: Julien C).");
             }
             if(string.IsNullOrEmpty(newUserInfos.prenom))
             {
                 ModelState.AddModelError("prenom", "Vous devez entrer un prénom.");
             }

            var file = Request.Files["userImage"];
            if((file != null && (file.ContentLength != 0 ))&& (!file.ContentType.StartsWith("image") || Path.GetExtension(file.FileName) == ""))
            {
                ModelState.AddModelError("userImage", "Le format du fichier est invalide.");
            }

            //Test si le mail est déja prit 
            //TODO: Ajaxer le test du mail déja prit
           /*
            IUserProfile tMailUser = Data.UserDataAccess.GetUserByMail(newUserInfos.email.Trim().ToLower());
            if (tMailUser != null && tMailUser.UserId != CurrentUser.UserId)
            {
                ModelState.AddModelError("email", "Cette adresse email est déjà associée à un autre compte.");
            }
           
            tMailUser = null; */
            if(ModelState.IsValid)
            {
                IUserProfile currUser = this.CurrentUser;
                /*
                bool emailChanged = false;
                if (currUser.Email.Trim().ToLower() != newUserInfos.email.Trim().ToLower())
                {
                    currUser.ActivationKey = Guid.NewGuid();
                    emailChanged = true;
                }
                */
                try
                {
                    currUser = newUserInfos.UpdateUserProfile(currUser, Data);
                    if(file != null && file.ContentLength != 0 )
                    {
                        currUser.ImagePersoPath = SaveUserImage(file, currUser);
                    }
                    //Update DB 
                    Data.UserDataAccess.Save();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("_FORM", "Vos modifications n'ont pas pu être effectuées.");
                    CovCake.Log.Error(ex.Message, ex);
                    return View(newUserInfos);
                }
                
                //refresh page ou vider cache client.
                this.ValidStateMessages.Add("Vos informations ont bien été modifiées.");
                /*
                if (emailChanged)
                {
                    var newMail= newUserInfos.email.Trim().ToLower();
                    this.CovCakeMailer.SendChangeEmailMail(currUser, newMail);
                    this.ValidStateMessages.Add("Vous avez changé votre adresse email, un email de confirmation vous a été envoyé.");

                }*/
                TempData["ViewInfos"] = this.ValidStateMessages;
                return RedirectToRoute(CovCake.Routes.MONCOMPTE);//"MonCompte");
            }
             
            return View(newUserInfos);
        }




        /// <summary>
        ///
        /// </summary>
        /// <param name="file"></param>
        /// <param name="currUser"></param>
        /// <returns></returns>
        private string SaveUserImage(HttpPostedFileBase file,IUserProfile currUser )
        {
                //TODO: génération d'un thumb ?
              
                string fullImageFolder = CovCakeConfiguration.UserPhotoStorageFolder;
                string newFilename = currUser.UserId.ToString() + Path.GetExtension(file.FileName);
                string clientSideFilename = fullImageFolder + "/" + newFilename;
                string serverSideFileName = this.GetFullServerPath(fullImageFolder + "/" + newFilename); //Path.Combine(fullImageFolder,newFilename);

                int fullWidth = int.Parse(CovCakeConfiguration.AppSetting("userPhotoFullWidth"));
                int fullHeight = int.Parse(CovCakeConfiguration.AppSetting("userPhotoFullHeight"));

           
                //int tinyWidth =int.Parse(Configuration.AppSetting("userPhotoTinyWidth"));
                //int tinyHeight = int.Parse(Configuration.AppSetting("userPhotoTinyHeight"));;
                try
                {
                    BitmapServices.ResizeAndSave(file.InputStream, fullWidth, fullHeight, true, serverSideFileName);
                }
                catch (Exception ex)
                {
                    clientSideFilename = "";
                    CovCake.Log.Error("Save user image Error", ex);
                }

                return clientSideFilename;
        }


    }
}
