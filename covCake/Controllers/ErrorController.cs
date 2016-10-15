using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace covCake.Controllers
{
    public class ErrorController : BaseController
    {
        //
        // GET: /Error/
        [PageTitle("Erreur")]
        public ActionResult Index(string aspxerrorpath)
        {
            return View("Error");
        }

        [PageTitle("Page non trouvée")]
        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewData["badurl"] =  this.Request.Url.Scheme+"://"+ this.Request.Url.Authority + aspxerrorpath;//this.Request.Url.PathAndQuery;

            //  ViewData["urlreferer"] = (this.Request.UrlReferrer != null) ? this.Request.UrlReferrer.PathAndQuery : "";
            this.Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound; 
     
            return View("Error404");
        }

        

    }
}
