using System;
using System.Globalization;
using System.Security.Principal;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using covCake.DataAccess;
using covCake.Models;
using covCake.Services;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace covCake.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None)]
    public class AccountController : BaseController
    {
        [PageTitle("Mot de passe oublié")]
        public ActionResult Forgot()
        {
            ForgotPassViewData forgot = new ForgotPassViewData();
            forgot.Succeed = false;
            return View(forgot);
        }


        [PageTitle("Mot de passe oublié")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Forgot(string email)
        {
            ForgotPassViewData forgot = new ForgotPassViewData();
            email = email.Trim();
            if (!CovCakeMailer.IsValidEmail(email))
            {
                ModelState.AddModelError("email", "L'adresse email est incorecte.");
            }

            IUserProfile userProfile = Data.UserDataAccess.GetUser(email);

            if (userProfile == null)
            {
                ModelState.AddModelError("email", "Aucun utilisateur avec cette adresse n'a été trouvé.");
            }

            if (ModelState.IsValid)
            {
                string infoMsgTitle = "Mot de passe réinitialisé";
                string infoMsg = string.Format("Un email a été envoyé à {0} avec un nouveau mot de passe.", email);
                //si pas encore activé envoi de activation
                //TODO: ajouter le user dans la table UserProfile des linscription et pas ke dans le membership
               /*
                if (!userProfile.GetMembershipUser().IsApproved && userProfile.ActivationKey.HasValue)
                {
                    this.CovCakeMailer.SendSignupMail(userProfile, userProfile.Email, "** votre mot de passe ***");
                    infoMsg = string.Format("L'email d'activation de compte viens d'être réenvoyé à {0}.", email);
                }
               */
                    string newPass = CovCakeServices.GenerateAlphaNumPass(int.Parse(CovCakeConfiguration.DefaultPasswordLenght));
                    newPass = newPass.ToLower();
                    MembershipUser membershipUser = userProfile.GetMembershipUser();
                    membershipUser.ChangePassword(membershipUser.ResetPassword(), newPass);
                    //   IUserProfile userProfile = Data.UserDataAccess.GetUser(email);
                    this.CovCakeMailer.SendForgotPasswordMail(userProfile, email, newPass);
                    forgot.Email = email;
                    forgot.Succeed = true;
               
                return Info(infoMsgTitle, infoMsg); //should redirect

                //return View(forgot);
            }
            else
            {
                // forgot.Succeed = false; 
                return View(forgot);
            }
        }

        [RedirectAuthorize]
        [PageTitle("Changer mon mot de passe")]
        public ActionResult ChangePassword(bool? fb)
        {
            ViewData["PasswordLength"] = this.MembershipUserManager.MinRequiredPasswordLength;
            if (fb.HasValue && fb.Value)
                return PartialView("ChangePasswordForm");

            return View();
        }

        [RedirectAuthorize]
        [PageTitle("Changer de mot de passe")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePassword(string currpass, string newpass, string confpass, bool? ajax)
        {
            int k = 0;
            ViewData["PasswordLength"] = this.MembershipUserManager.MinRequiredPasswordLength;

            // Basic parameter validation
            if (String.IsNullOrEmpty(confpass))
            {
                string REZMESSAGE = "Entrez votre mot de passe actuel.";

                if (ajax.GetValueOrDefault())
                    return Json(new { code = ++k, msg = REZMESSAGE });

                ModelState.AddModelError("currpass", REZMESSAGE);
            }

            if (newpass == null || newpass.Length < this.MembershipUserManager.MinRequiredPasswordLength)
            {
                string REZMESSAGE = string.Format("Le mot de passe doit être composé d'au moins {0} caractères.",
                                  this.MembershipUserManager.MinRequiredPasswordLength);

                if (ajax.GetValueOrDefault())//(bool.Parse(fawk))//.HasValue && ajax.Value)
                    return Json(new { code = ++k, msg = REZMESSAGE });

                ModelState.AddModelError("newpass", REZMESSAGE);
            }

            if (!String.Equals(newpass, confpass, StringComparison.Ordinal))
            {
                string REZMESSAGE = "Le nouveau mot de passe et sa confirmation sont différents.";

                if (ajax.GetValueOrDefault())
                    return Json(new { code = ++k, msg = REZMESSAGE });

                ModelState.AddModelError("_FORM", REZMESSAGE);// "Le nouveau mot de passe et sa confirmation sont différents.");
            }

            if (ModelState.IsValid)
            {
                // Attempt to change password
                MembershipUser membershipUser = this.CurrentUser.GetMembershipUser();// this.MembershipUserManager.GetUser(User.Identity.Name, true /* userIsOnline */);
                bool changeSuccessful = false;
                try
                {
                    changeSuccessful = membershipUser.ChangePassword(currpass, newpass);
                }
                catch
                {
                    // An exception is thrown if the new password does not meet the provider's requirements
                }

                if (changeSuccessful)
                {
                    /*
                    string REZMESSAGE = "Mot de passe modifié correctement.";
                    if (ajax.GetValueOrDefault())
                        return Json(new { code = 0, msg = REZMESSAGE });
                     * */
                    //return RedirectToRoute(CovCake.Routes.MONCOMPTE);
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    string REZMESSAGE = "Le mot de passe actuel est incorrect ou le nouveau mot de passe est invalide.";
                    if (ajax.GetValueOrDefault())
                        return Json(new { code = ++k, msg = REZMESSAGE });

                    ModelState.AddModelError("_FORM", REZMESSAGE);
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [PageTitle("Changer mon mot de passe")]
        public ActionResult ChangePasswordSuccess()
        {

            return View();
        }

        [PageTitle("Connexion")]
        public ActionResult Login(string ruri, bool? fb)
        {
            ViewData["ReturnUrl"] = ruri;
            if (fb.HasValue && fb.Value)
                return PartialView("LoginForm");
            return View();
        }

        [PageTitle("Connexion")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            username = username.Trim();
            string msgErr = "L'adresse email ou le mot de passe est incorecte ou bien votre compte n'a pas encore été validé.";

            // Basic parameter validation
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "Vous devez entrer une adresse email.");
            }

            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "Vous devez entrer votre mot de passe.");
            }

            if (ViewData.ModelState.IsValid)
            {
                // Attempt to login

                bool loginSuccessful = this.MembershipUserManager.ValidateUser(username, password);

                if (loginSuccessful)
                {

                    this.SetAuthCookie(username, CovCakeConfiguration.RememberLogin);

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToRoute(CovCake.Routes.HOMEINDEX);
                    }
                }
                else
                {
                    ModelState.AddModelError("_FORM", msgErr);
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        /// <summary>
        /// Log out et redirect vers page d'accueil
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            this.ForceUserLogOut();

            return this.HomeIndex();//RedirectToAction("Index", "Home");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        [PageTitle("Inscription")]
        public ActionResult Inscription()
        {
            ViewData["PasswordLength"] = this.MembershipUserManager.MinRequiredPasswordLength;
            return View();
        }

        [PageTitle("Inscription")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Inscription(UserInfosViewData registerData)
        {
            //string nom = form["nom"].ToTitleCase();
            //string prenom = form["prenom"].ToTitleCase();
            //string age_jour = form["age_jour"];
            //string age_mois = form["age_mois"];
            //string age_annee = form["sexe"];
            //string sexe = form["age_annee"];
            //string numdept = form["numdept"];
            //string ville = form["ville"];
            //string password = form["password"];
            //string confirmPassword = form["confirmPassword"];
            //string email = form["email"].ToLower();

            string nom = registerData.nom.ToTitleCase().Trim();
            string prenom = registerData.prenom.ToTitleCase().Trim();
            string age_jour = registerData.age_jour;
            string age_mois = registerData.age_mois;
            string age_annee = registerData.age_annee;
            string sexe = registerData.sexe;
            string numdept = registerData.numdept;
            string ville = registerData.ville.ToTitleCase().Trim();
            string password = registerData.password;
            string confirmPassword = registerData.confirmPassword;
            string email = registerData.email.ToLower().Trim();

            ViewData["PasswordLength"] = this.MembershipUserManager.MinRequiredPasswordLength;

            // Basic parameter validation
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "Vous devez entrer une adresse email.");
            }
            else if (!CovCakeMailer.IsValidEmail(email))
            {
                ModelState.AddModelError("email", "Cette adresse email est invalide.");
            }
            if (password == null || password.Length < this.MembershipUserManager.MinRequiredPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "Le mot de passe doit être composé d'au moins {0} caracteres.",
                         this.MembershipUserManager.MinRequiredPasswordLength));
            }
            if (string.IsNullOrEmpty(nom))
            {
                ModelState.AddModelError("nom", "Vous devez entrer un nom, seule son initiale sera affichée (ex: Julien C).");
            }
            if (string.IsNullOrEmpty(prenom))
            {
                ModelState.AddModelError("prenom", "Vous devez entrer un prénom.");
            }

            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("confirmPassword", "Le mot de passe et sa confirmation ne correspondent pas.");
            }

            if (ViewData.ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;

                MembershipUser newUser = this.MembershipUserManager.CreateUser(email, password, email, null, null, false, null, out createStatus);


                if (newUser != null)
                {
                    using (TransactionScope trans = new TransactionScope())
                    {
                        Guid userID = (Guid)newUser.ProviderUserKey;
                        int ageAnnee = 1900 + int.Parse(age_annee);
                        DateTime dateNaiss = new DateTime(ageAnnee, int.Parse(age_mois), int.Parse(age_jour));
                        IUserProfile user = Data.UserDataAccess.CreateUser(userID, nom, prenom, dateNaiss, bool.Parse(sexe));
                        user.NumDepartement = numdept;
                        user.Ville = ville;
                        user.ActivationKey = Guid.NewGuid();
                        //Insert in table
                        Data.UserDataAccess.InsertUser(user);
                        ViewData["email"] = email;

                        if (CovCakeConfiguration.ConfirmRegistration)
                        {
                            //envoi de mail de confirm
                            bool rezsend = this.CovCakeMailer.SendSignupMail(user, email, password);

                            if (rezsend == false)
                            {
                                return Error("Une erreur est survenue lors de votre enregistrement.");
                            }
                            trans.Complete();
                            return View("PendingRegistration", user);
                        }
                        else
                        {
                            //Envoi mail de bienvenue
                            this.CovCakeMailer.SendSignupConfirmation(user, email, password);
                            //activation directe
                            this.ActivateAccount(user.ActivationKey.Value);

                            trans.Complete();
                            return HomeIndex();  //RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    this.SetModelError(createStatus);
                }
            }

            // If we got this far, something failed, redisplay form
            // UpdateModel(registerData);
            return View(registerData);
        }

        /// <summary>
        /// View, procedure activation de compte
        /// </summary>
        /// <param name="ac">actkey</param>
        /// <returns></returns>
        public ActionResult Activate(string ac)
        {

            IUserProfile user = this.ActivateAccount(ac.ToGuid());
            if (user == null)
            {
                var errorMsg = "Cette clée d'activation est invalide ou a expirée.";
                return Error(errorMsg);
            }

            //authentification
            this.SetAuthCookie(user.Email, CovCakeConfiguration.RememberLogin /* createPersistentCookie */);
            //TODO: Ou rediriger apres l'activation
            return RedirectToRoute(CovCake.Routes.HOMEINDEX);
        }


        private IUserProfile ActivateAccount(Guid actKey)
        {
            IUserProfile user = Data.UserDataAccess.GetUserByActKey(actKey);
            MembershipUser memUser = Membership.GetUser(user.UserId, false);
            if (!memUser.IsApproved)
            {
                //Activation du l'user
                memUser.UnlockUser();
                memUser.IsApproved = true;
                Membership.UpdateUser(memUser);
            }
            return user;
        }


        [RedirectAuthorize]
        public ActionResult ChangeEmail(bool? fb)
        {
            if (fb.HasValue && fb.Value)
            {
                ViewData["email"] = this.CurrentUser.Email;
                return PartialView("ChangeEmail");
            }
            return Error(); // PartialView("ErrorPartial");
        }

        [RedirectAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangeEmail(string newmail)
        {

            string REZMESSAGE = "";
            IUserProfile tMailUser = Data.UserDataAccess.GetUserByMail(newmail.ToLower());
            if (String.IsNullOrEmpty(newmail))
            {
                REZMESSAGE = "Vous devez entrer une adresse email.";
                return Json(new { code = "1", msg = REZMESSAGE });
            }
            else if (!CovCakeMailer.IsValidEmail(newmail))
            {
                REZMESSAGE = "Cette adresse email est invalide.";
                return Json(new { code = "2", msg = REZMESSAGE });
            }


            IUserProfile currUser = this.CurrentUser;
            currUser.ActivationKey = Guid.NewGuid();

            Data.UserDataAccess.Save();

            var newMail = newmail.ToLower();
            this.CovCakeMailer.SendChangeEmailMail(currUser, newMail);
            this.ValidStateMessages.Add("Vous avez souhaité changer votre adresse email, un email de confirmation vous a été envoyé.");

            return RedirectToRoute(CovCake.Routes.MONCOMPTE);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ak">clé d'activation shrinké</param>
        /// <param name="nm"></param>
        /// <returns></returns>
        public ActionResult SetNewMail(string ak, string nm)
        {
            string errMsg = "Impossible d'activer votre nouvelle adresse email.";

            if (ak.IsNullOrEmpty() || nm.IsNullOrEmpty())
            {
                // ViewData["ErrorMsg"] = errMsg; //"Impossible d'activer votre nouvelle adresse email";
                return Error(errMsg);
            }

            Guid actKey = ak.ToGuid();
            nm = nm.UnShrinkForUrl();

            if (!Mailer.IsValidEmail(nm) || actKey == Guid.Empty)
            {
                return Error(errMsg);
            }

            try
            {
                //this.ForceUserLogOut();
                nm = nm.ToLower();

                IUserProfile user = Data.UserDataAccess.GetUserByActKey(actKey);
                if (user == null)
                {
                    return Error(errMsg);
                }

                //Test si l'email est déjà utilisé
                IUserProfile testMailUser = Data.UserDataAccess.GetUserByMail(nm);
                if (testMailUser != null && testMailUser.UserId != user.UserId)
                {
                    string errMsg2 = nm + ", Cette adresse email est déjà utilisée.";
                    return Error(errMsg2);
                }
                testMailUser = null;

                MembershipUser membershipUser = user.GetMembershipUser();
                membershipUser.Email = nm;
                Membership.UpdateUser(membershipUser);

                //user.Email = nm;
                user.UserName = nm; //modifie aussi le userName dans membership
                user.ActivationKey = null; //on efface le ActivationKey  
                //Data.UserDataAccess.Save();// save du username


                Data.CovCakeDataContext.SubmitChanges(ConflictMode.ContinueOnConflict);


                //   if (this.CurrentUserId == user.UserId)

                this.CovCakeMailer.SendChangeEmailConfirmation(user, nm);
                this.ForceUserLogIn(user.UserId); //log l'utilisateur de force


            }
            catch (ChangeConflictException e)
            {
                var db = Data.CovCakeDataContext;

                CovCake.Log.Info(e.Message);
                foreach (ObjectChangeConflict occ in db.ChangeConflicts)
                {
                    //No database values are merged into current.
                    occ.Resolve(RefreshMode.KeepCurrentValues);
                }
                /*
                
                CovCake.Log.Info("Optimistic concurrency error.");
               CovCake.Log.Info(e.Message);
               
                foreach (ObjectChangeConflict occ in db.ChangeConflicts)
                {
                    MetaTable metatable = db.Mapping.GetTable(occ.Object.GetType());
                    //cov_UserProfile entityInConflict = (Customer)occ.Object;
                    CovCake.Log.Info(String.Format("Table name: {0}", metatable.TableName));
                   CovCake.Log.Info( String.Format("Customer ID: "));
                   // Console.WriteLine(entityInConflict.CustomerID);
                    foreach (MemberChangeConflict mcc in occ.MemberConflicts)
                    {
                        object currVal = mcc.CurrentValue;
                        object origVal = mcc.OriginalValue;
                        object databaseVal = mcc.DatabaseValue;
                        MemberInfo mi = mcc.Member;
                        CovCake.Log.Info( String.Format("Member: {0}", mi.Name));
                       CovCake.Log.Info(  String.Format("current value: {0}", currVal));
                       CovCake.Log.Info( String.Format("original value: {0}", origVal));
                       CovCake.Log.Info( String.Format("database value: {0}", databaseVal));
                    }
                }*/
            }
            catch (Exception ex)
            {
                CovCake.Log.Error(ex.Message, ex);
                return Error(errMsg);
            }



            this.ValidStateMessages.Add("Votre Adresse email a bien été changée!");
            TempData["ViewInfos"] = this.ValidStateMessages;
            return RedirectToRoute(CovCake.Routes.MONCOMPTE);// RedirectToAction("MonCompte", "User");//MonCompte(null, null, null, null);

        }


        private void SetModelError(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            string labelId = "_FORM";
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                //return "Ce nom d'utilisateur est déjà pris";

                case MembershipCreateStatus.DuplicateEmail:
                    labelId = "email";
                    // return "Un autre utilisateur utilise déjà cette adresse email.";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    labelId = "password";
                    break;
                // return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidUserName:

                case MembershipCreateStatus.InvalidEmail:
                    labelId = "email";
                    break;
                case MembershipCreateStatus.ProviderError:

                case MembershipCreateStatus.UserRejected:
                default:
                    break;
            }
            ModelState.AddModelError(labelId, ErrorCodeToString(createStatus));
        }


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                //return "Ce nom d'utilisateur est déjà pris";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Un autre utilisateur utilise déjà cette adresse email.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Aucune compte ne correspond à l'adresse spéficiée";//"The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:

                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }


        #region Register original
        /*
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(string username, string email, string password, string confirmPassword)
        {

            ViewData["Title"] = "Register";
            ViewData["PasswordLength"] = Provider.MinRequiredPasswordLength;

            // Basic parameter validation
            if (String.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("username", "You must specify a username.");
            }
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "You must specify an email address.");
            }
            if (password == null || password.Length < Provider.MinRequiredPasswordLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "You must specify a password of {0} or more characters.",
                         Provider.MinRequiredPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "The new password and confirmation password do not match.");
            }

            if (ViewData.ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                MembershipUser newUser = Provider.CreateUser(username, password, email, null, null, true, null, out createStatus);

                if (newUser != null)
                {
                    FormsAuth.SetAuthCookie(username, false);// createPersistentCookie );
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("_FORM", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
*/
        #endregion


    }

    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.
    /*
    public interface IFormsAuthentication
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationWrapper : IFormsAuthentication
    {
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
     * */
}
