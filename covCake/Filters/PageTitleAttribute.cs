using System;
using System.Web.Mvc;
using System.Diagnostics;

namespace covCake
{
    [DebuggerStepThrough]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class PageTitleAttribute : ActionFilterAttribute
    {

        private string _title;
        
        public PageTitleAttribute(string title)
        {
            this._title = title + " ¤ " + CovCakeConfiguration.SiteSlogan;
        }


        /// <summary>
        /// Spécifie le titre de la page rendue
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
           
            filterContext.Controller.ViewData["title"] = _title.SwapIfEmpty(CovCakeConfiguration.SiteName);

        }

        
    }
}
