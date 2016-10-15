using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using covCake.Controllers;
using covCake.DataAccess;

namespace covCake
{
    public class BaseViewPage<T> : ViewPage<T> where T : class
    {

        public BaseController Controller
        {
            get
            {
                if (ViewContext.Controller is BaseController)
                {
                    return (BaseController)ViewContext.Controller;
                }
                return null;
            }
        }


        public bool IsAuthenticated
        {
            get { return Request.IsAuthenticated; }
        }

        public string UserPhotoUrl
        {
            get
            {

                return ((BaseController)ViewContext.Controller).UserPhotoUrl;
            }
        }

        public IUserProfile CurrentUser
        {
            get
            {
                try
                {
                    if (ViewContext.Controller is BaseController)
                    {
                        return ((BaseController)ViewContext.Controller).CurrentUser;
                    }
                    return null;
                }
                catch (Exception ex) 
                {
                    return null; 
                }
            }
        }
        public Guid CurrentUserId
        {
            get
            {
                try
                {
                    if (ViewContext.Controller is BaseController)
                    {
                        return ((BaseController)ViewContext.Controller).CurrentUserId;
                    }
                    return Guid.Empty;
                }
                catch (Exception ex) 
                { 
                    return Guid.Empty; 
                }
            }
        }
    }


    public class BaseViewPage : ViewPage
    {
        public BaseController Controller
        {
            get
            {
                if (ViewContext.Controller is BaseController)
                {
                    return (BaseController)ViewContext.Controller;
                }
                return null;
            }
        }

        public bool IsAuthenticated
        {
            get { return Request.IsAuthenticated; }
        }

        public string UserPhotoUrl
        {
            get
            {
                return ((BaseController)ViewContext.Controller).UserPhotoUrl;
            }
        }

        public IUserProfile CurrentUser
        {
            get
            {
                try
                {
                    if (ViewContext.Controller is BaseController)
                    {
                        return ((BaseController)ViewContext.Controller).CurrentUser;
                    }
                    return null;
                }
                catch (Exception ex) 
                { 
                    return null; 
                }
            }
        }
    }

    public static class ViewPageExtensions
    {
        /// <summary>
        /// Ajoute aux entetes de réponse Pragma no-cache
        /// </summary>
        /// <param name="vp"></param>
        public static void SetNoCache(this ViewPage vp)
        {
            vp.Response.CacheControl = "no-cache";
            vp.Response.AddHeader("Pragma", "no-cache");
            vp.Response.ExpiresAbsolute = DateTime.Now.Date;
            vp.Response.Expires = -1;
        }


    }

}
