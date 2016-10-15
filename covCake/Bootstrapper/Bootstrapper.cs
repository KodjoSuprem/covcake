using System.Web.Mvc;
using System.Threading;
using System.Web.Routing;
using System.Web;
using System.Text.RegularExpressions;

namespace covCake
{
    public static class Bootstrapper
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
  
            //routes.MapRoute("Page de maintenance", "", new { controller = "Home", action = "Maintenance" });
            routes.MapRoute(CovCake.Routes.HOMEINDEX, "", new { controller = "Home", action = "Index" });

            routes.MapRoute(CovCake.Routes.MONCOMPTE,"MonCompte",new { controller = "User", action = "MonCompte"});
            routes.MapRoute(CovCake.Routes.MONCOMPTEEDIT, "MonCompte/EditInfos", new { controller = "User", action = "EditInfos" });

            routes.MapRoute(CovCake.Routes.PROJETLIST, "Voyages/{page}", new { controller = "Projets", action = "Liste", page="" });
            routes.MapRoute(CovCake.Routes.PROJETINDEX, "Voyages/View/{projetId}", new { controller = "Projets", action = "Index" }, new { projetId = @"\d+" });

            routes.MapRoute(CovCake.Routes.USERINDEX, "User/Profile/{userId}", new { controller = "User", action = "Index", userId = "" });//, new { userId = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$" });
           
            routes.MapRoute(CovCake.Routes.ACTIVATEACCOUNT, "Activate/{ac}", new { controller = "Account", action = "Activate", ac = "" });

            routes.MapRoute("Erreur 404", "404/", new { controller = "Error", action = "NotFound"});

            routes.MapRoute(CovCake.Routes.SHOWMESSAGE, "Messages/{msgId}", new { controller = "Messages", action = "ShowMessage" }, new { msgId = @"\d+" });
            routes.MapRoute(CovCake.Routes.MESSAGESINDEX, "Messages", new { controller = "Messages", action = "Index" });
            
            routes.MapRoute(CovCake.Routes.CONTACTPAGE, "Contact", new { controller = "Home", action = "Contact" });

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

            /*
            routes.MapRouteLowercase(
              "Default",                                              // Route name
              "{controller}/{action}/{id}",                           // URL with parameters
              new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
          );
 */

        }


        public static void InitCultureInfo()
        {
            Thread.CurrentThread.CurrentCulture = CovCake.FRCulture;
        }

        public static void InitCovCake()
        {
            InitCultureInfo();
          //  ModelBinders.Binders[typeof(Invoice)] = new InvoiceBinder();
            ModelBinders.Binders.DefaultBinder = new TrimerDefaultModelBinder();
 
            
            RegisterRoutes(RouteTable.Routes);
        }

        public static void LowerCaseIncomingUrl(HttpRequest Request, HttpResponse Response)
        {
            
            string lowercaseURL = (Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.AbsolutePath);
            if (Regex.IsMatch(lowercaseURL, @"[A-Z]")) //si ya au moins une majuscule
            {
                lowercaseURL = lowercaseURL.ToLower() + HttpContext.Current.Request.Url.Query;

                //Response.Headers["Location"] = lowercaseURL;
                // Response.End();
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", lowercaseURL);
                Response.End();
              
    
            }
        }

       
    }
}
