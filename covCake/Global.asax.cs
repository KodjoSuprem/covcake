
using System;
namespace covCake
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        // If upper case letters are found in the URL, redirect to lower case URL.
        // Was receiving undesirable results here as my QueryString was also being converted to lowercase.
        // You may want this, but I did not.
        //if (Regex.IsMatch(HttpContext.Current.Request.Url.ToString(), @"[A-Z]") == true)
        //{
        //    string LowercaseURL = HttpContext.Current.Request.Url.ToString().ToLower();

        //    Response.Clear();
        //    Response.Status = "301 Moved Permanently";
        //    Response.AddHeader("Location", LowercaseURL);
        //    Response.End();
        //}

        // If upper case letters are found in the URL, redirect to lower case URL (keep querystring the same).
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

           // Bootstrapper.LowerCaseIncomingUrl(Request, Response);
        }


        protected void Application_Start()
        {
            Bootstrapper.InitCovCake();
        }
    }
}