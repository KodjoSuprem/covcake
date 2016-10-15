
using System.Web.Mvc;
using System;
namespace covCake
{

    /// <summary>
    /// Log Automatiquement toutes les exceptions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AutoLogExceptionsAttribute : FilterAttribute, IExceptionFilter 
    {
        
        public void OnException(ExceptionContext filterContext)
        {
            //string route = "Route ";
            //filterContext.RouteData.Values.ForEach(r => { route += r.Key + "=" + r.Value.ToString() + ", "; });
            string param = "Params: ";
            filterContext.Controller.ValueProvider.ForEach(p => { param += p.Key + "=" + p.Value.AttemptedValue + ", "; });

            CovCake.Log.Error(param, filterContext.Exception);
            //  throw new System.NotImplementedException();
        }
        /*
        private void  OnException(ExceptionContext filterContext)
        {
            string route = "Route: ";
            string handled = (filterContext.ExceptionHandled) ? "handled" : "";
            filterContext.RouteData.Values.ForEach( o => { route += o.Key +"="+o.Value.ToString()+", ";});
            if (filterContext.ExceptionHandled)
                CovCake.Log.TextLogger.cError(route,"handled", filterContext.Exception);
            else 
                CovCake.Log.Exception.cError(route, filterContext.Exception);
 	      //  throw new System.NotImplementedException();
        }*/

        }
}
