using System.Web.Mvc;

namespace covCake.Controllers
{
    public class PartialLoaderController : Controller
    {
        public ActionResult Index(string viewName)
        {
            // Add action logic here
            return PartialView(viewName);
          }
        /*
        public ActionResult Login()
        {
            // Add action logic here
            string partielView = "LoginForm";
            return RedirectToAction("Index","PartialLoaderController",partielView);
        }
        public ActionResult Register()
        {
            // Add action logic here
            string partielView = "RegisterForm";
            return RedirectToAction("Index", "PartialLoaderController", partielView);
        }
        public ActionResult ChangePassword()
        {
            // Add action logic here
            string partielView = "ChangePasswordForm";
            return RedirectToAction("Index", "PartialLoaderController", partielView);
        }

        */
    }
}
