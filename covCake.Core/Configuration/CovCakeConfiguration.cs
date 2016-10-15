using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace covCake
{
    [DebuggerStepThrough]
    public class CovCakeConfiguration 
    {
        private static  int CACHE_EXPIRATION = 24;
        public static int DefaultDataCacheExpiration
        {
            get { return CACHE_EXPIRATION; }
        }


        public static bool ConfirmRegistration
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["confirmRegistration"]); }
        }

        /// <summary>
        /// Delais en seconde pour l'envoi des notifications de subscribtion par email
        /// </summary>
        public static int SubscriptionNotificationDelay
        {
            get { return int.Parse(ConfigurationManager.AppSettings["subscriptionNotificationDelay"]); }
        }

        public static bool RememberLogin
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["rememberLogin"]); }
        }

        public static string SmtpHost
        {
            get { return (ConfigurationManager.AppSettings["smtpHost"]); }
        }

        public static string SmtpLogin
        {
            get { return (ConfigurationManager.AppSettings["smtpLogin"]); }
        }

        public static string SmtpPass
        {
            get { return (ConfigurationManager.AppSettings["smtpPass"]); }
        }

        public static bool SmtpSSL
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["smtpSSL"]); }
        }
        

        public static string DefaultPasswordLenght
        {
            get { return (ConfigurationManager.AppSettings["defaultPasswordLenght"]); }
        }


        public static string SiteHost
        {
            get
            {
                #if DEBUG
                return "localhost";
                #else
                return ConfigurationManager.AppSettings["siteHost"];
                #endif
            }
        }

        public static string SiteName
        {
            get 
            {  
                #if DEBUG
                return "localhost";//"localhost:51761";
                #else
                return ConfigurationManager.AppSettings["siteName"]; 
                #endif
            }

        }
        

        public static string SiteProjectAlertEmail
        {
            get { return ConfigurationManager.AppSettings["siteProjectAlertEmail"]; }
        }

        public static string SiteNotifierEmail
        {
            get { return ConfigurationManager.AppSettings["siteNotifierEmail"]; }
        }

        public static string SiteUrl
        {
            get { return ConfigurationManager.AppSettings["siteUrl"]; }
        }

        public static string SiteAdminEmail
        {
            get { return ConfigurationManager.AppSettings["siteAdminEmail"]; }
        }

        public static string AlertSenderPassword
        {
            get { return ConfigurationManager.AppSettings["alertSenderPass"]; }
        }

        public static string SiteContactEmail
        {
            get { return ConfigurationManager.AppSettings["siteContactEmail"]; }
        }


        /// <summary>
        /// Slogan du site à mettre dans les titres de page
        /// </summary>
        public static string SiteSlogan
        {
            get { return ConfigurationManager.AppSettings["siteSlogan"]; }
        }

        public static string UserPhotoStorageFolder
        {
            get { return ConfigurationManager.AppSettings["userPhotoStorageFolder"]; }
        }

        public static string PhotoStoragePath
        {
            get { return ConfigurationManager.AppSettings["photoStoragePath"]; }
        }

        public static string DefaultUserPhoto
        {
            get 
            {
                return UserPhotoStorageFolder + "/" + ConfigurationManager.AppSettings["defaultUserPhotoName"];
            }
        }


        public static string AppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
