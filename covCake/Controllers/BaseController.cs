using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Security;
using covCake.DataAccess;
using covCake.Services;
using System.Web.Mvc.Html;
using covCake.Models;

namespace covCake.Controllers
{
    /// <summary>
    /// Base class which contains common stuffs.
    /// </summary>
   // [GetViewInfos]
    [AutoTrimActionParams]
    [AutoLogExceptions]
    [HandleError]
    [Compress]
    public abstract class BaseController : Controller
    {
        public const string USER_SESSION_KEY = "user";

        private IUserProfile _currentUser; //A pas mettre en static sinon même user pr tt les sessions
      //  private HtmlHelper _htmlhelper;
        //public Dictionary<string, string> ValidStateMessages { get; private set; }

        //Stock les messages d'informations à afficher dans la vue
        public List<string> ValidStateMessages { get; private set; }

        protected static CovCakeData Data { get {  return CovCake.DataProvider; }  }

        //public HtmlHelper Html { get; private set; }
        protected MembershipProvider MembershipUserManager { get; private set; }

        protected CovCakeMailer CovCakeMailer { get;private set;}


        protected BaseController(MembershipProvider userManager)
        {
            this.MembershipUserManager = userManager;
           // _htmlhelper = = new HtmlHelper(this.vi
        }


        protected BaseController(): this(Membership.Provider)
        {
            this.CovCakeMailer = new CovCakeMailer(this);
            this.ValidStateMessages = new List<string>();
        }

     
        protected MembershipUser CurrentMembershipUser
        {
            get { return CovCake.GetCurrentUser(); }
        }

        #region Default View results
        /// <summary>
        /// Redirige vers une page d'erreur
        /// </summary>
        /// <param name="ErrorMessage">Message d'erreur à afficher</param>
        /// <returns></returns>
        [PageTitle("Erreur")]
        protected ActionResult Error(string ErrorMessage)
        {
            ViewData["ErrorMsg"] = ErrorMessage;
            return View("Error");
        }

        /// <summary>
        /// Redirige vers une page d'erreur
        /// </summary>
        /// <returns></returns>
        protected ActionResult Error()
        {
            return Error("");
        }

        /// <summary>
        /// Redirige vers une page d'informations
        /// </summary>
        /// <param name="infoTitle"></param>
        /// <param name="infoMsg"></param>
        /// <returns></returns>
        protected ActionResult Info(string infoTitle, string infoMsg)
        {
            ViewData["InfoMsgTitle"] = infoTitle;
            return View("Info");
        }
      

        protected ActionResult ErrorRedirect(string errormsg, int delayseconds, string redirectUrl)
        {
            ErrorRedirectViewData errorViewData = new ErrorRedirectViewData()
            {
                DelaySeconds = delayseconds,
                ErrorMsg = errormsg,
                RedirectUrl = redirectUrl
            };

            return View("ErrorRedirect",errorViewData);
        }

        protected ActionResult LoginPage()
        {
            return RedirectToRoute(CovCake.Routes.HOMEINDEX);
        }


        protected ActionResult HomeIndex()
        {
            return RedirectToRoute(CovCake.Routes.HOMEINDEX);
        }
        #endregion


        //protected void OnException(ExceptionContext filterContext)
        //{
        //    string route = "Route: ";
        //    filterContext.RouteData.Values.ForEach(o => { route += o.Key + "=" + o.Value.ToString() + ", "; });
        //    CovCake.Log.Exception.cError(route, filterContext.Exception);
        //    //  throw new System.NotImplementedException();
        //    base.OnException(filterContext);
        //}
        



        /// <summary>
        /// Authentifie l'utilisateur au site 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        protected bool ForceUserLogIn(Guid UserId)
        {
            try
            {
                 MembershipUser user = this.MembershipUserManager.GetUser(UserId, true);
                this.SetAuthCookie(user.UserName, CovCakeConfiguration.RememberLogin);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        [NonAction]
        protected void ForceUserLogOut()
        {
            CovCake.LogoutCurrentUser();
            this._currentUser = null;

        }

        protected void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

   

        /// <summary>
        /// Gets a value indicating whether the currently visiting user is authenticated.
        /// </summary>
        /// <value>
        /// if user authenticated returns <c>true</c>; otherwise, <c>false</c>.
        /// </value>
        public bool IsUserAuthenticated
        {
            [DebuggerStepThrough]
            get
            {
                return HttpContext.User.Identity.IsAuthenticated;
            }
        }

        /*
        public string HtmlEncode(string s)
        {
            return Server.HtmlEncode(s);
        }
        */
    

        public string GetFullServerPath(string path)
        {
            return Server.MapPath(path);
        }

        protected string GetRelativeServerPath(string fullpath)
        {
            return fullpath.Replace(Server.MapPath("~"), "");
        }

        //protected string GetClientSidePath(string fullpath)
        //{
          //  return GetRelativeServerPath(fullpath);
       // }

        public virtual string GetAbsolutePath(string relativeUrl)
        {
            UriBuilder uriBuilder = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port);


            uriBuilder.Path = relativeUrl;

            return uriBuilder.Uri.ToString();
        }

        protected void SetPageTitle(string title)
        {
            ViewData["Title"] = title + " ¤ " + CovCakeConfiguration.SiteSlogan;
        }


        public string UserPhotoUrl
        {
            get
            {
                IUserProfile user = this.CurrentUser;
                if (user != null)
                {
                    if(System.IO.File.Exists(Server.MapPath(user.ImagePersoPath)))
                    {

                        return user.ImagePersoPath;
                    }
                }
                return CovCakeConfiguration.DefaultUserPhoto;
            }
        }

        public IUserProfile CurrentUser
        {
            get
            {
                return GetCurrentUser();
            }
        }

        private IUserProfile GetCurrentUser()
        {
                if (base.User.Identity.IsAuthenticated)
                {
                    
                    if (_currentUser == null)
                    {
                        _currentUser = Data.UserDataAccess.GetUser(this.CurrentUserId);
                        if (_currentUser == null)
                        {
                            //Authentifié mais le l'ID retourné n'existe plus dans la base des UserProfiles
                            //Remove les cookies
                            this.ForceUserLogOut();
                        }
                    }
                }
                else
                {
                    _currentUser = null;
                }
                return _currentUser;
        }

        private IUserProfile GetCurrentUserInCache()
        {
           
                if (base.User.Identity.IsAuthenticated)
                {
                    IUserProfile user = this.Session[USER_SESSION_KEY] as IUserProfile;
                    if (user == null)
                    {
                        _currentUser = Data.UserDataAccess.GetUser(this.CurrentUserId);
                        if (_currentUser == null)
                        {
                            //Authentifié mais le l'ID retourné n'existe plus dans la base des UserProfiles
                            //Remove les cookies
                            this.ForceUserLogOut();
                        }
                        else
                        {
                            this.Session[USER_SESSION_KEY] = _currentUser;
                        }
                    }
                    else
                    {
                        this._currentUser = user;
                    }
                }
                else
                {
                    _currentUser = null;
                }
            
            return _currentUser;
        }

        /// <summary>
        /// Gets the current user id. if the user is not authenticated it returns empty guid.
        /// </summary>
        /// <value>The current user id.</value>
        public Guid CurrentUserId
        {
            [DebuggerStepThrough]
            get
            {
                if (!IsUserAuthenticated)
                    return Guid.Empty;
              //  var user = this.MembershipUserManager.GetUser(this.CurrentUserName, true);
                return CovCake.GetCurrentUserId();
            }
        }

        protected string RequestIP
        {
            get
            {
                return HttpContext.Request.UserHostAddress;
            }
        }


        public new RedirectToRouteResult RedirectToAction(string actionName, string controllerName)
        {
            return base.RedirectToAction(actionName, controllerName);
        }
        public new RedirectToRouteResult RedirectToAction(string actionName)
        {
            return base.RedirectToAction(actionName);
        }
        public new RedirectToRouteResult RedirectToAction(string actionName, object values)
        {
            return base.RedirectToAction(actionName, values);
        }
        public new RedirectToRouteResult RedirectToAction(string actionName, string controllerName, object values)
        {
            return base.RedirectToAction(actionName, controllerName, values);
        }

   




        //public virtual DateTime ConvertToLocalTime(DateTime dateTime)
        //{
        //    if (ControllerContext.HttpContext.User == null)
        //    {
        //        if (Config.Site.TimeZoneOffset != 0)
        //        {
        //            return dateTime.Add(TimeSpan.FromHours(Config.Site.TimeZoneOffset));
        //        }
        //        else
        //        {
        //            return dateTime;
        //        }
        //    }
        //    else
        //    {
        //        return dateTime; //TODO: (erikpo) Get the timezone offset from the current user and apply it
        //    }
        //}

        //public virtual DateTime ConvertFromLocalTime(DateTime dateTime)
        //{
        //    if (ControllerContext.HttpContext.User == null)
        //    {
        //        if (Config.Site.TimeZoneOffset != 0)
        //        {
        //            return dateTime.Subtract(TimeSpan.FromHours(Config.Site.TimeZoneOffset));
        //        }
        //        else
        //        {
        //            return dateTime;
        //        }
        //    }
        //    else
        //    {
        //        return dateTime; //TODO: (erikpo) Get the timezone offset from the current user and apply it
        //    }
        //}
    }
}