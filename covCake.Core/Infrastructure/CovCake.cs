using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Globalization;
using covCake.DataAccess;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace covCake
{
    /// <summary>
    /// Méthodes statiques et constantes de l'application
    /// </summary>
    public static class CovCake
    {
        public static class Routes
        {
            public const string MONCOMPTEEDIT   = "MonCompteEdit";
            public const string MONCOMPTE       = "MonCompte";
            public const string HOMEINDEX       = "HomeIndex";
            public const string CONTACTPAGE     = "Contact";
            public const string ACTIVATEACCOUNT = "ActivateAccount";
            public const string ERRORROUTE      = "ErrorRoute";
            //public const string MESSAGEINDEX    = "MessageIndex";
            
            public const string USERINDEX       = "UserIndex";
            public const string PROJETINDEX     = "ProjetIndex";
            public const string PROJETLIST      = "ProjetList";
            public const string MESSAGESINDEX   = "MessagesIndex";
            public const string SHOWMESSAGE     = "ShowMessage";
        }

        public static class Debug
        {
            public static void WriteLine(string msg)
            {
                System.Diagnostics.Debugger.Log(0, "DEBUG", msg);
            }
        }

        public static class Log
        {
            private static ILog _exception;
            private static ILog _all;
            private static ILog _mail;
            static Log()
            {
                log4net.Config.XmlConfigurator.Configure();
                log4net.GlobalContext.Properties["label"] = "GENERAL";
               // string userName = (CovCake.GetCurrentUser() != null ) ? CovCake.GetCurrentUser().Email : "ANONYMOUS";
                //_exception = Log.Exception;
                //_all = Log.TextLogger;
                //_mail = Log.Mail;

            }

            public static ILog TextLogger
            {
                //Pas degueux car singleton, si déjà instencié, renvoi l'instance ;)
                get { return LogManager.GetLogger("TextLogger"); }
            }
            public static ILog Mail
            {
                //Pas degueux car singleton, si déjà instencié, renvoi l'instance ;)
                get { return LogManager.GetLogger("MailLogger"); }
            }

            public static ILog Exception
            {
                //Pas degueux car singleton, si déjà instencié, renvoi l'instance ;)
                get { return LogManager.GetLogger("ExceptionLogger"); }
            }

            public static ILog NewUser
            {
                //Pas degueux car singleton, si déjà instencié, renvoi l'instance ;)
                get { return LogManager.GetLogger("NewUserLogger"); }
                
            }

            public static void Info(string label,string msg )
            {
          
                if(!label.IsNullOrEmpty())
                    log4net.ThreadContext.Properties["label"] = label.ToUpper() ;
                Log.TextLogger.Info(msg);
                log4net.ThreadContext.Properties.Clear();
         
            }

            
            public static void Info(string msg)
            {
                Info(null,msg );
            }

            public static void MailInfo(string msg, string subjectDetails)
            {
                log4net.ThreadContext.Properties["mailsubject"] = subjectDetails;
                Log.Mail.Info(msg);
                log4net.ThreadContext.Properties.Clear();
            }

            [System.Diagnostics.DebuggerStepThrough]
            public static void Error(string msg, Exception ex)
            {
                Log.Exception.Error(msg,ex);
            }

            public static void NewUserMail(IUserProfile user)
            {
                string msg = user.PrenomNom + " - " + user.Email;
                Log.NewUser.Info(msg);
            }

        }

        private static CovCakeData _dataProvider = new CovCakeData();

        public static CultureInfo FRCulture = new CultureInfo("fr-FR");
        public static CovCakeData DataProvider { get { return _dataProvider; } }

      
        public static void Cache(string cacheKey, object value, DateTime absoluteDateExpiration)
        {
            CacheManager.Insert(cacheKey, value, absoluteDateExpiration);
        }

        public static void Cache(string cacheKey, object value, TimeSpan timeOutExpiration)
        {
            CacheManager.Insert(cacheKey, value, timeOutExpiration);
        }

        public static object UnCache(string cacheKey)
        {
           return CacheManager.Get(cacheKey);
        }

        public static T UnCache<T>(string cacheKey)
        {
            return (T)CacheManager.Get(cacheKey);
        }

        public static MembershipUser GetCurrentUser()
        {
            return System.Web.Security.Membership.GetUser();
        }
        public static Guid GetCurrentUserId()
        {
            try
            {
                return (Guid)GetCurrentUser().ProviderUserKey;
            }
            catch
            {
            }
            return Guid.Empty;
        }
        public static string GetCurrentUserEmail()
        {
            return (CovCake.GetCurrentUser() != null) ? CovCake.GetCurrentUser().Email : "Anonymous";
        }

        public static void LogoutCurrentUser()
        {
            FormsAuthentication.SignOut();
        }

        public static void Write(string str)
        {
            HttpContext.Current.Response.Write(str);
        }

        public static void WriteLine(string str)
        {
            Write(str + Environment.NewLine);
        }
    }
}
