
using System.Web.Mvc;
using covCake.Controllers;
using System.Collections;
using System.Collections.Generic;
using System;

namespace covCake
{

    public class GetViewInfosAttribute : ActionFilterAttribute
    {
        public readonly string DEFAULT_VIEWINFOS_KEY = "ViewInfos";

        private string _viewInfosKey;
        /// <summary>
        /// "ViewInfos" as default ViewInfosKey :
        /// </summary>
        public GetViewInfosAttribute()
        {
            this._viewInfosKey = DEFAULT_VIEWINFOS_KEY;
        }

        /// <summary>
        /// Set your own ViewInfos key
        /// </summary>
        /// <param name="viewInfosKey"></param>
        public GetViewInfosAttribute(string viewInfosKey)
        {
            this._viewInfosKey = viewInfosKey;
        }

        /// <summary>
        /// Copie TempData dans viewData
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
          
            if (filterContext.Controller is BaseController)
            {
                var baseController = filterContext.Controller as BaseController;
                if (baseController != null)
                {
                    if (filterContext.Controller.TempData.ContainsKey(this._viewInfosKey))
                    {
                        filterContext.Controller.ViewData[this._viewInfosKey] = filterContext.Controller.TempData[this._viewInfosKey];
                    }
                    if (filterContext.Controller.TempData[this._viewInfosKey] is IEnumerable<string>)
                    {
                        IEnumerable<string> infoMessages = filterContext.Controller.TempData[this._viewInfosKey] as IEnumerable<string>;
                        baseController.ValidStateMessages.AddRange(infoMessages);
                    }
                    else if (filterContext.Controller.TempData[this._viewInfosKey] is string)
                    {
                        string infoMessage = filterContext.Controller.TempData[this._viewInfosKey] as string;
                        baseController.ValidStateMessages.Add(infoMessage);
                    }
                }
            }

  
            base.OnActionExecuted(filterContext);

        }
    }
}
