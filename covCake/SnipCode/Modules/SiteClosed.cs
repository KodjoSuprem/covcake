using System.Web;
using System;
using System.Net;

namespace covCake
{
    public class SiteClosed : BaseHttpModule
    {
        public override void OnBeginRequest(HttpContextBase context)
        {
            /*
            Uri requestedUrl = context.Request.Url;
            string assets = "{0}://{1}/Assets".FormatWith(requestedUrl.Scheme, requestedUrl.Host);

            if (!requestedUrl.ToString().StartsWith(assets, StringComparison.OrdinalIgnoreCase))
            {


                throw new HttpException((int) HttpStatusCode.ServiceUnavailable, "We are currently doing some maintenance. Please check back soon.");
            }
             * */

           
            //TODO: débloquer le site pour les requettes vers les assets
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Server.Transfer("~/ErrorPages/SiteClosed.aspx");
        }
    }
}