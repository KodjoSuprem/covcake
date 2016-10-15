using System.Web.Mvc;
using covCake.Controllers;

namespace covCake
{
    /// <summary>
    /// Merge le ModelStateDictionnary du TempData["ModelError"] à celui de l'action en cours 
    /// </summary>
    public class GetModelErrorAttribute : ActionFilterAttribute
    {
        public  const string MODEL_STATE_DICTIONARY_DEFAULT_KEY = "ModelError";
        private string _modelErrorKey;
        public GetModelErrorAttribute()
        {
            this._modelErrorKey = MODEL_STATE_DICTIONARY_DEFAULT_KEY;
        }
        public GetModelErrorAttribute(string modelStateDictionaryKey)
        {
            this._modelErrorKey = modelStateDictionaryKey;
        }
       
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller.TempData.ContainsKey(this._modelErrorKey))
            {
                if (filterContext.Controller is BaseController)
                {
                    var baseController = filterContext.Controller as BaseController;
                    if (baseController != null)
                    {
                        ModelStateDictionary errorDico = (ModelStateDictionary)filterContext.Controller.TempData[this._modelErrorKey];
                        baseController.ModelState.Merge(errorDico);
                    }
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
