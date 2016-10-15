using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Routing;
using covCake.Controllers;
using covCake.DataAccess;
using covCake.Helpers;

namespace covCake.Services
{
    public class CovCakeMailer : Mailer
    {

        public BaseController RefController;
        public string MailTemplateFolder { get; set; }

        public CovCakeMailer(BaseController controler)
            : base(CovCakeConfiguration.SmtpSSL)
        {
            this.RefController = controler;
            this.MailTemplateFolder = "~/MailTemplates";

        }

        
       /*
        public CovCakeMailer()
            : base(CovCakeConfiguration.SmtpHost, CovCakeConfiguration.SmtpLogin, CovCakeConfiguration.SmtpPass, CovCakeConfiguration.SmtpSSL)
        {
            this.MailTemplateFolder = "~/MailTemplates";
        }
        */
      

        public CovCakeMailer(string username, string password)
            : base(CovCakeConfiguration.SmtpHost, username, password)
        {
            this.MailTemplateFolder = "~/MailTemplates";
        }

        public CovCakeMailer(string hostAddress, string username, string password)
            : base(hostAddress, username, password)
        {
            this.MailTemplateFolder = "~/MailTemplates";
        }
        public string LoadMailTemplate(string templateName)
        {
            //    _templateDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templateDirectory);
            string templateDirectory = this.MailTemplateFolder + "\\" + templateName;
            return LoadMailTemplate(templateDirectory, templateName);
        }

        public string LoadMailTemplate(string path, string cacheKey)
        {

            string body = (string)CacheManager.Get(cacheKey);// HttpRuntime.Cache[cacheKey];

            if (body == null)
            {
                string file = this.RefController.Server.MapPath(path);
                body = File.ReadAllText(file);
                CacheManager.Insert(cacheKey, body, DateTime.Now.AddHours(24));
                //HttpRuntime.Cache.Insert(cacheKey, body, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return body;
        }

        public bool SendChangeEmailMail(IUserProfile user, string newEmail)
        {
            const string templateName = "ChangeMail.txt";
            try
            {
                bool IsHTML = templateName.Contains(".htm");
                string fromEmail = CovCakeConfiguration.SiteAdminEmail;
                string fromEmailName = "CoVoyage.net";
                string subject = "CoVoyage: Changement d'adresse email";
                string toEmail = newEmail;
                string body = this.LoadMailTemplate(templateName);

                RouteValueDictionary linkValues = new RouteValueDictionary 
                { 
                    { "ak", user.ActivationKey.Value.Shrink() }, 
                    { "nm", newEmail.ShrinkForUrl() }
                };

                string confirmationUrl = this.RefController.Url.CovCakeActionUrl("SetNewMail", "Account", linkValues);

                body = body.Replace("#DISPLAYNAME#", user.Prenom);
                body = body.Replace("#NEWEMAIL#", newEmail);

                body = body.Replace("#ACTIVATIONURL#", confirmationUrl);


                var from = new MailAddress(fromEmail, fromEmailName);

                this.SendMailAsync(from, toEmail, subject, body, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendChangeEmailMail userId=" + CovCake.GetCurrentUserId().ToString(), ex);
                return false;
            }
            return true;
        }

        public bool SendForgotPasswordMail(IUserProfile user, string email, string newPassword)
        {

            const string templateName = "Forgot.txt";
            try
            {
                string siteName = CovCakeConfiguration.SiteName;
                string siteUrl = CovCakeConfiguration.SiteUrl;
                string fromEmail = CovCakeConfiguration.SiteAdminEmail;
                string fromEmailName = "CoVoyage.net";
                string subject = "Réinitialisation de votre mot de passe Covoyage";

                bool IsHTML = templateName.Contains(".htm");

                string body = this.LoadMailTemplate(templateName);

                body = body.Replace("#SITENAME#", siteName);
                body = body.Replace("#DISPLAYNAME#", user.Prenom);
                body = body.Replace("#EMAIL#", email);
                body = body.Replace("#PASSWORD#", newPassword);
                body = body.Replace("#TITLE#", subject);
                body = body.Replace("#WEBSITEURL#", siteUrl);
              
                var from = new MailAddress(fromEmail, fromEmailName);

                this.SendMail(from, email, subject, body, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendForgotPasswordMail", ex);

                return false;
            }
            return true;
        }

        public void SendContactMail(string nom, string email, string sujet, string message, string Ip)
        {
            const string templateName = "Contact.txt";

            //string siteName = CovCakeConfiguration.SiteName;
            string toEMAIL = CovCakeConfiguration.SiteContactEmail;
            //string fromEmailName = siteName;
            string subject = "Contact: " + sujet;

            string body = this.LoadMailTemplate(templateName);
            body = body.Replace("#NAME#", nom);

            body = body.Replace("#SUBJECT#", sujet);
            body = body.Replace("#EMAIL#", email);
            body = body.Replace("#MESSAGE#", message);
            body = body.Replace("#IPADDRESS#", Ip);
            body = body.Replace("#DATETIME#", DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());

            nom = nom.Trim().RemoveSpecialChars();
            MailAddress fromAddress = new MailAddress(email, nom);
            this.SendMailAsync(fromAddress, toEMAIL, subject, body, false);
        }

        public bool SendSignupMail(IUserProfile user, string email, string password)
        {
            const string templateName = "Signup.txt";
            // const string mailTemplatePath = "~/MailTemplates/Signup.txt";
            bool IsHTML = templateName.Contains(".htm");

            MailAddress FromEMAILADDRESS = new MailAddress("inscription@covoyage.net", "Inscription sur CoVoyage");
            string ToEMAILADDRESS = email;

            string SUBJECT = "Activation de votre compte CoVoyage";

            string MAILBODY = this.LoadMailTemplate(templateName);
            string activationUrl = this.RefController.Url.CovCakeRouteUrl(CovCake.Routes.ACTIVATEACCOUNT, new { ac = user.ActivationKey.Value.Shrink() });

            MAILBODY = MAILBODY.Replace("#SITENAME#", CovCakeConfiguration.SiteName);
            MAILBODY = MAILBODY.Replace("#DISPLAYNAME#", user.Prenom);
            MAILBODY = MAILBODY.Replace("#EMAIL#", email);
            MAILBODY = MAILBODY.Replace("#PASSWORD#", password);
            MAILBODY = MAILBODY.Replace("#ACTIVATIONURL#", activationUrl);
            MAILBODY = MAILBODY.Replace("#TITLE#", SUBJECT);

            try
            {
                this.SendMail(FromEMAILADDRESS, ToEMAILADDRESS, SUBJECT, MAILBODY, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendSignupMail userId=" + CovCake.GetCurrentUserId().ToString(), ex);
                return false;
            }
            return true;
        }

        public bool SendNewMessageNotify(IMessagePrive msg)
        {
            const string templateName = "PrivateMessageNotify.txt";
            // const string mailTemplatePath = "~/MailTemplates/Signup.txt";

            bool IsHTML = templateName.Contains(".htm");
            string siteName = CovCakeConfiguration.SiteName;
            string from = CovCakeConfiguration.SiteNotifierEmail;
            string fromEmailName = siteName;
            string toEmail = msg.ToUser.Email;
            string subject = "CoVoyage.net : " + msg.FromUser.PrenomNom + " vous a envoyé un message sur CoVoyage";

            // msgResponseLink = CovCakeConfiguration.SiteUrl + msgResponseLink;
            // msgResponseLink = this.Url.CovCakeActionUrl("ShowMessage", "Messages", new RouteValueDictionary { { "MsgId", newMsgId } }, "http", CovCakeConfiguration.SiteName);

            string msgResponseLink = this.RefController.Url.CovCakeActionUrl("ShowMessage", "Messages", new { MsgId = msg.MsgId });

            string body = this.LoadMailTemplate(templateName);


            string relatedProjSentence = "";
            if (msg.ProjetRelatedId != null)
                relatedProjSentence = "concernant le voyage " + msg.ProjetRelated.GetShortDisplayName();


            body = body.Replace("#DISPLAYNAME#", msg.ToUser.Prenom);
            body = body.Replace("#SENDERDISPLAYNAME#", msg.FromUser.Prenom);

            body = body.Replace("#MESSAGE#", msg.TextMessage);
            body = body.Replace("#SUBJECT#", msg.SujetMessage);
            body = body.Replace("#SHOWMESSAGELINK#", msgResponseLink);

            body = body.Replace("#RELATEDTOPROJETSENTENCE#", relatedProjSentence);

            try
            {
                MailAddress fromAddress = new MailAddress(from, fromEmailName);
                //   this.To.Add(new MailAddress(email));
                this.SendMailAsync(fromAddress, toEmail, subject, body, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendNewMessageNotify userId=" + CovCake.GetCurrentUserId().ToString(), ex);
                return false;
            }
            return true;

        }

        public bool SendProjectResignNotify(IAbonnementProjet abo)
        {
            const string templateName = "ProjectResignNotify.htm";
            try
            {
                IUserProfile toUser = abo.Projet.OwnerUserProfile;
                IUserProfile fromUser = abo.UserProfile;
                bool IsHTML = templateName.Contains(".htm");
                string siteName = CovCakeConfiguration.SiteName;
                string from = CovCakeConfiguration.SiteNotifierEmail;
                string fromEmailName = siteName;
                string toEmail = toUser.Email;
                string subject = "CoVoyage.net : " + fromUser.PrenomNom + " a quitté un de vos voyages";

                string subscriberUrl = this.RefController.Url.CovCakeActionUrl("Index", "User", new { userId = toUser.UserId });

                string ProfileLink = "<a href='" + subscriberUrl + "' >" + fromUser.PrenomNom + "</a>";

                string ProjetUrl = this.RefController.Url.CovCakeActionUrl("Index", "Projets", new { projetId = abo.ProjetId });
                string ProjetLink = "<a href='" + ProjetUrl + "' >" + abo.Projet.GetLongDisplayName() + "</a>";

                string body = this.LoadMailTemplate(templateName);

                body = body.Replace("#DISPLAYNAME#", toUser.Prenom);
                body = body.Replace("#PROJETLONGNAMELINK#", ProjetLink);

                body = body.Replace("#SUBSCRIBERNAMELINK#", ProfileLink);

                MailAddress fromAddress = new MailAddress(from, fromEmailName);
                //   this.To.Add(new MailAddress(email));
                this.SendMailAsync(fromAddress, toEmail, subject, body, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError(string.Format("{0} userid={1}", "SendProjectResignNotify", CovCake.GetCurrentUserId().ToString()), ex);
                return false;
            }
            return true;

        }

        public bool SendFriendsProjectSubscribNotify(IAbonnementProjet newAbo)
        {
            const string templateName = "ProjectFriendSubscribNotify.htm";
            try
            {
                IQueryable<IAbonnementProjet> otherAboList = newAbo.Projet.UserAbonnes;
                IUserProfile toOwner = newAbo.Projet.OwnerUserProfile;
                IUserProfile fromSubscriber = newAbo.UserProfile;
                bool IsHTML = templateName.Contains(".htm");
                string siteName = CovCakeConfiguration.SiteName;
                string from = CovCakeConfiguration.SiteNotifierEmail;
                string fromEmailName = siteName;
                string toEmail, toDisplayName;
                string subject = "CoVoyage.net : " + fromSubscriber.PrenomNom + " vous rejoint sur un voyage";

                string body = this.LoadMailTemplate(templateName);

                string subscriberUrl = this.RefController.Url.CovCakeActionUrl("Index", "User", new { userId = toOwner.UserId });
                string ProfileLink = "<a href='" + subscriberUrl + "' >" + fromSubscriber.PrenomNom + "</a>";

                string ProjetUrl = this.RefController.Url.CovCakeActionUrl("Index", "Projets", new { projetId = newAbo.ProjetId });
                string ProjetLink = "<a href='" + ProjetUrl + "' >" + newAbo.Projet.GetLongDisplayName() + "</a>";

                int nbSubs = otherAboList.Count() + 1;
                body = body.Replace("#PROJETLONGNAMELINK#", ProjetLink);
                body = body.Replace("#SUBSCRIBERNAMELINK#", ProfileLink);
                body = body.Replace("#NBSUBSCRIBERS#", nbSubs.ToString());

                foreach (var abo in otherAboList)
                {
                    if (newAbo.ProjetId == abo.ProjetId && newAbo.UserId == abo.UserId)
                        continue;

                    toEmail = abo.UserProfile.Email;
                    toDisplayName = abo.UserProfile.Prenom;

                    body = body.Replace("#DISPLAYNAME#", toDisplayName);

                    this.SendMailAsync(new MailAddress(from, fromEmailName), toEmail, subject, body, IsHTML);

                }
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendFriendsProjectSubscribNotify error", ex);
                return false;
            }
            return true;
        }

        public bool SendProjectSubscribtionNotify(IAbonnementProjet abo)
        {
            const string templateName = "ProjectSubscribtionNotify.htm";
            try
            {
                bool IsHTML = templateName.Contains(".htm");
                IUserProfile toOwner = abo.Projet.OwnerUserProfile;
                IUserProfile fromSubscriber = abo.UserProfile;

                string FromEMAILNAME = CovCakeConfiguration.SiteName;
                string FromEMAIL = CovCakeConfiguration.SiteNotifierEmail;
                MailAddress FromADDRESS = new MailAddress(FromEMAIL, FromEMAILNAME);

                string ToEMAIL = toOwner.Email;
                string SUBJECT = "CoVoyage.net : " + fromSubscriber.PrenomNom + " a rejoint un de vos voyages";

                string subscriberUrl = this.RefController.Url.CovCakeRouteUrl(CovCake.Routes.USERINDEX, new { userId = toOwner.UserId.Shrink() });
                string ProfileLink = "<a href='" + subscriberUrl + "' >" + fromSubscriber.PrenomNom + "</a>";

                string ProjetUrl = this.RefController.Url.CovCakeRouteUrl(CovCake.Routes.PROJETINDEX, new { projetId = abo.ProjetId });
                string ProjetLink = "<a href='" + ProjetUrl + "' >" + abo.Projet.GetLongDisplayName() + "</a>";
                string MAILBODY = this.LoadMailTemplate(templateName);

                var ReplaceArray = new Dictionary<string, string>();
                ReplaceArray["#DISPLAYNAME#"] = toOwner.Prenom;
                ReplaceArray["#PROJETLONGNAMELINK#"] = ProjetLink;
                ReplaceArray["#SUBSCRIBERNAMELINK#"] = ProfileLink;
                MAILBODY = MAILBODY.Replace(ReplaceArray);

                this.SendMailAsync(FromADDRESS, ToEMAIL, SUBJECT, MAILBODY, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendProjectSubscribtionNotify error", ex);
                return false;
            }
            return true;
        }

        public bool SendProjetEditNotify(IProjet proj)
        {
            const string templateName = "ProjectEditNotify.htm";

            try
            {
                bool IsHTML = templateName.Contains(".htm");
                string siteName = CovCakeConfiguration.SiteName;
                string from = CovCakeConfiguration.SiteNotifierEmail;
                string subject = "CoVoyage.net : le voyage auquel vous participez a été modifié";

                string fromEmailName = siteName;
                string toEmail;

                string body = this.LoadMailTemplate(templateName);

                string projetUrl = this.RefController.Url.CovCakeActionUrl("Index", "Projets", new { projetId = proj.IdProjet });
                string projetLink = "<a href='" + projetUrl + "' >" + proj.GetLongDisplayName() + "</a>";

                string projetOwnerUrl = this.RefController.Url.CovCakeRouteUrl(CovCake.Routes.USERINDEX, new { userId = proj.OwnerUserProfile.UserId.Shrink() });
                string projetOwnerLink = "<a href='" + projetOwnerUrl + "' >" + proj.OwnerUserProfile.PrenomNom + "</a>";

                body = body.Replace("#PROJETLONGNAMELINK#", projetLink);
                body = body.Replace("#PROJETOWNERNAMELINK#", projetOwnerLink);
                foreach (var abo in proj.UserAbonnes)
                {
                    body = body.Replace("#DISPLAYNAME#", abo.UserProfile.Prenom);
                    toEmail = abo.UserProfile.Email;
                    this.SendMailAsync(new MailAddress(from, fromEmailName), toEmail, subject, body, IsHTML);
                }
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendProjetEditNotify Error. IdProjet=" + proj.IdProjet.ToString(), ex);
                return false;
            }
            return true;
        }

        public bool SendProjetAlertMail(IUserProfile toUser, IEnumerable<IProjet> projetAlerts, int concerningNbAlerts)//, IEnumerable<IAlerte> alertesUsed)
        {
            const string templateName = "ProjectAlertList.htm";
            const string projectItemTemplate = "ProjectAlertItem.htm";
            try
            {
                bool IsHTML = templateName.Contains(".htm");
                string FromEMAILNAME = "CoVoyage Alertes Voyages";
                string FromEMAIL = CovCakeConfiguration.SiteProjectAlertEmail;
                MailAddress FromEMAILADDRESS = new MailAddress(FromEMAIL, FromEMAILNAME);

                string SUBJECT = "Votre sélection de voyages sur CoVoyage.net";

                string ToEMAIL = toUser.Email;
                MailAddress ToEMAILADDRESS = new MailAddress(ToEMAIL);


                string MAILBODY = this.LoadMailTemplate(templateName);

                string MonCompteUrl = this.RefController.Url.CovCakeRouteUrl(CovCake.Routes.MONCOMPTE);

                StringBuilder PROJECTITEMS = new StringBuilder();
                string itemTemplate = this.LoadMailTemplate(projectItemTemplate);
                Dictionary<string, string> ReplaceArray;
                foreach (IProjet item in projetAlerts)
                {
                    //Setting an item
                    string ProjectUrl = this.RefController.Url.CovCakeRouteUrl(CovCake.Routes.PROJETINDEX, new { projetId = item.IdProjet });

                    ReplaceArray = new Dictionary<string, string>();

                    ReplaceArray["#DATEDEBUT#"] = item.GetShortDateStringOrDuration();
                    ReplaceArray["#DURATION#"] = item.GetDuree().ToString();
                    //    RA["#DATEFIN#"]                    =   item.GetDuree().ToString();        
                    ReplaceArray["#OWNERDISPLAYNAME#"] = item.OwnerUserProfile.DisplayName;
                    ReplaceArray["#NBSUBSCRIBERS#"] = (item.UserAbonnes.Count() + 1).ToString(); //Abonnes plus le owner
                    ReplaceArray["#PROJECTTITLE#"] = item.GetProjectTitle();
                    ReplaceArray["#PROJECTURL#"] = ProjectUrl;
                    ReplaceArray["#PROJECTID#"] = item.IdProjet.ToString();
                    ReplaceArray["#PROJECTCOMMENTS#"] = item.Commentaires;
                    ReplaceArray["#CHANGEALERTS#"] = MonCompteUrl;

                    PROJECTITEMS.AppendLine(itemTemplate.Replace(ReplaceArray));

                }

                ReplaceArray = new Dictionary<string, string>();

                //Header
                ReplaceArray["#DISPLAYNAME#"] = toUser.DisplayName;

                //List
                ReplaceArray["#PROJECTLIST#"] = PROJECTITEMS.ToString();

                //Footer
                ReplaceArray["#NBALERTS#"] = concerningNbAlerts.ToString();
                ReplaceArray["#MONCOMPTE#"] = MonCompteUrl;

                MAILBODY = MAILBODY.Replace(ReplaceArray);

                MailMessage MAILMESSAGE = new MailMessage()
                {
                    From = FromEMAILADDRESS,
                    Subject = SUBJECT,
                    Body = MAILBODY,
                    // BodyEncoding = Encoding.GetEncoding("iso-8859-1"),
                    IsBodyHtml = IsHTML
                };

                MAILMESSAGE.To.Add(ToEMAILADDRESS);


                this.SendMailAsync(MAILMESSAGE);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendProjetAlertMail Error. ToUserId=" + toUser.UserId.ToString(), ex);
                return false;
            }
            return true;
        }


        public bool SendSignupConfirmation(IUserProfile user, string email, string password)
        {
            const string templateName = "SignupConfirm.txt";
            // const string mailTemplatePath = "~/MailTemplates/Signup.txt";
            bool IsHTML = templateName.Contains(".htm");

            MailAddress FromEMAILADDRESS = new MailAddress("inscription@covoyage.net", "Inscription sur CoVoyage");
            string ToEMAILADDRESS = email;

            string SUBJECT = "Bienvenue sur CoVoyage.net";

            string MAILBODY = this.LoadMailTemplate(templateName);

            // MAILBODY = MAILBODY.Replace("#SITENAME#", CovCakeConfiguration.SiteName);
            MAILBODY = MAILBODY.Replace("#DISPLAYNAME#", user.Prenom);
            MAILBODY = MAILBODY.Replace("#EMAIL#", email);
            MAILBODY = MAILBODY.Replace("#PASSWORD#", password);
            //  MAILBODY = MAILBODY.Replace("#TITLE#", SUBJECT);

            try
            {
                //   this.To.Add(new MailAddress(email));
                this.SendMail(FromEMAILADDRESS, ToEMAILADDRESS, SUBJECT, MAILBODY, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError("SendSignupConfirmation userId=" + CovCake.GetCurrentUserId().ToString(), ex);
                return false;
            }
            return true;
        }

        public bool SendChangeEmailConfirmation(IUserProfile user, string newEmail)
        {
            const string templateName = "ChangeMailConfirmation.txt";
            try
            {

                bool IsHTML = templateName.Contains(".htm");
                string fromEmail = CovCakeConfiguration.SiteAdminEmail;
                string fromEmailName = "CoVoyage.net";
                string subject = "Changement d'adresse email";
                string toEmail = newEmail;
                string body = this.LoadMailTemplate(templateName);

                body = body.Replace("#DISPLAYNAME#", user.Prenom);
                body = body.Replace("#NEWEMAIL#", newEmail);

                var from = new MailAddress(fromEmail, fromEmailName);

                this.SendMailAsync(from, toEmail, subject, body, IsHTML);
            }
            catch (Exception ex)
            {
                CovCake.Log.Exception.cError(ex.Message, ex);
                return false;
            }
            return true;
        }
    }
}
