using System.Web.Mvc;
using covCake.Controllers;
using covCake.DataAccess;

namespace covCake
{
    public class BaseViewUserControl<T> : ViewUserControl<T> where T : class
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


        public IUserProfile CurrentUser
        {
            get
            {
                if (ViewContext.Controller is BaseController)
                {
                    return ((BaseController)ViewContext.Controller).CurrentUser;
                }

                return null;
            }
        }
        public string UserPhotoUrl
        {
            get
            {

                return ((BaseController)ViewContext.Controller).UserPhotoUrl;
            }
        }
    }
    public class BaseViewUserControl : ViewUserControl
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

        public IUserProfile CurrentUser
        {
            get
            {
                if (ViewContext.Controller is BaseController)
                {
                    return ((BaseController)ViewContext.Controller).CurrentUser;
                }

                return null;
            }
        }
        public string UserPhotoUrl
        {
            get
            {

                return ((BaseController)ViewContext.Controller).UserPhotoUrl;
            }
        }
    }
}
