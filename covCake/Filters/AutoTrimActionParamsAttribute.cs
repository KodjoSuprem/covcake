using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Diagnostics;

namespace covCake
{
    /// <summary>
    /// Trim tout les paramètres de type String de l'action
    /// </summary>
    [DebuggerStepThrough]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AutoTrimActionParamsAttribute : ActionFilterAttribute
    {
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<string> keyList = new List<string>(context.ActionParameters.Keys);
           
            foreach (string key in keyList)
	        {
                if (context.ActionParameters[key] is string)
                {
                    context.ActionParameters[key] = ((string)context.ActionParameters[key]).Trim();
                }
	        }
        }
    }
}
