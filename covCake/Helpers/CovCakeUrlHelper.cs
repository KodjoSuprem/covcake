using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using covCake.DataAccess;

namespace covCake.Helpers
{
    public static class CovCakeUrlHelper
    {


        public static string UserProfileUrl(this UrlHelper url, IUserProfile user)
        {
           return  url.RouteUrl(CovCake.Routes.USERINDEX, new { userId = user.UserId.Shrink() });

        }

        /// <summary>
        /// retourne une url covoyage complete type http://www.covoyage.net/controler/action
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string CovCakeActionUrl(this UrlHelper url, string actionName, string controllerName, RouteValueDictionary routeValues)
        {

            //TODO: A refaire Vieille rustine parce ke ASP.net c naz..
            //en esperant ke sa marche sur IIS
            string urlRelative = url.Action(actionName, controllerName, routeValues);
            string host = "http://localhost:51761";
            try
            {
                if (url.RequestContext.HttpContext.Request.Url == null)
                {
                  
                    return host + urlRelative; //System.IO.Path.Combine(host, urlRelative);
                }
            }
            catch (Exception ex)
            {
                return host + urlRelative; //System.IO.Path.Combine(host, urlRelative);
            }
            return url.Action(actionName, controllerName, routeValues, "http", CovCakeConfiguration.SiteHost);
     
       }
        /// <summary>
        /// retourne une url covoyage complete type http://www.covoyage.net/controler/action
        /// </summary>
        /// <param name="url"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string CovCakeActionUrl(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {
       
            return url.CovCakeActionUrl(actionName, controllerName, new RouteValueDictionary(routeValues));

        }

        public static string CovCakeActionUrl(this UrlHelper url, string routeName, object routeValues)
        {

            return url.CovCakeActionUrl( routeName, new RouteValueDictionary(routeValues));

        }

        public static string CovCakeRouteUrl(this UrlHelper url, string routeName)
        {
            return url.CovCakeRouteUrl(routeName, null);
        }

        public static string CovCakeRouteUrl(this UrlHelper url, string routeName, object routeValues)
        {
          
            //TODO: A refaire Vieille rustine parce ke ASP.net c naz..
            //en esperant ke sa marche sur IIS
            string urlRelative = url.RouteUrl(routeName, routeValues);
            string host = "http://localhost:51761";
            try
            {
                if (url.RequestContext.HttpContext.Request.Url == null)
                {

                    return host + urlRelative; //System.IO.Path.Combine(host, urlRelative);
                }
            }
            catch (Exception ex)
            {
                return host + urlRelative; //System.IO.Path.Combine(host, urlRelative);
            }
            return url.RouteUrl(routeName, new RouteValueDictionary(routeValues), "http", CovCakeConfiguration.SiteHost);

        }


        /// <summary>
        /// Non implémenté
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string Asset(this UrlHelper helper, string assetName)
        {
            //TODO: customiser le assets helper
            throw new NotImplementedException();
            //return helper.Content("~/asset.axd") + "?name={0}&v={1}".FormatWith(assetName, AssetHandler.GetVersion(assetName));
        }

        /// <summary>
        /// Non implémenté, A implémenter avec le systeme d'Assets
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Image(this UrlHelper helper, string fileName)
        {

            //TODO: customiser  le assets helper
            throw new NotImplementedException();
           // return helper.Content("~/Assets/Images/{0}".FormatWith(fileName));
        }
    }
}
