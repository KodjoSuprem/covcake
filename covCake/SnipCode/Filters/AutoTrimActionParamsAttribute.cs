using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Diagnostics;

namespace covCake
{
    /// <summary>
    /// Trim tout les paramètres de type String de l'action
    /// </summary>
    //[DebuggerStepThrough]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AutoTrimActionParamsAttribute : ActionFilterAttribute
    {
        private string _excludeParams;
        private List<string> _excludeList;
        public string Exclude 
        {
            get { return _excludeParams; }
            set 
            { 
                _excludeParams = value.ToLower();
                _excludeList = new List<string>(_excludeParams.Split(';'));
                _excludeList.ForEach(op => op = op.ToLower().Trim());
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            List<string> keyList = new List<string>(context.ActionParameters.Keys);
           
            foreach (string key in keyList)
	        {
                //if (!_excludeList.Contains(key.ToLower()))
                //{
                    if (context.ActionParameters[key] is string)
                    {

                        context.ActionParameters[key] = ((string)context.ActionParameters[key]).Trim();

                    }
                //}
	        }
        }
    }
}
