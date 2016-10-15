using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using covCake.Controllers;

namespace covCake
{
  [DebuggerStepThrough]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
  public class RedirectAuthorizeAttribute : AuthorizeAttribute 
  {

      public override void OnAuthorization(AuthorizationContext filterContext)
      {
          if (filterContext.Controller is BaseController)
          {
              var baseController = filterContext.Controller as BaseController;
              if (baseController != null)
              {
                  if (!baseController.User.Identity.IsAuthenticated)
                  {
                      Uri requestedUrl = filterContext.HttpContext.Request.Url;

                      //TODO: Pour les modal boxes renvoyer juste une partial. if (fb) new {fb=true}
                      //chemin relatif pr la sécurité
                      string relativeUri = requestedUrl.PathAndQuery;
                      filterContext.Result = baseController.RedirectToAction("Login", "Account", new { ruri = relativeUri });

                  }
              }
          }
          base.OnAuthorization(filterContext);
      }

      protected override bool AuthorizeCore(HttpContextBase httpContext)
      {
          if (httpContext == null)
          {
              throw new ArgumentNullException("httpContext");
          }
          IPrincipal user = httpContext.User;
          if (!user.Identity.IsAuthenticated)
          {
              return false;
          }
          return true;
      }


      protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
      {
          if (httpContext == null)
          {
              throw new ArgumentNullException("httpContext");
          }
          if (!this.AuthorizeCore(httpContext))
          {
              return HttpValidationStatus.IgnoreThisRequest;
          }
          return HttpValidationStatus.Valid;
      }
  }

  
}
