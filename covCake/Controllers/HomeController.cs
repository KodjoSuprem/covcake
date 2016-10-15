using System;
using System.Web.Mvc;
using covCake.Services;

namespace covCake.Controllers
{


    public class HomeController : BaseController
    {
        
        public ActionResult Index(int? page)
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to ASP.NET MVC!";// page=" + Url.RouteUrl(CovCake.Routes.HOMEINDEX);




            //if (this.IsUserAuthenticated)
            //{
            //    var user = this.CurrentUser;

            //    user.Nom += "1";
            //    Data.SubmitChanges();

            //    var user2 = this.CurrentUser;
            //}

        
            return View();
        }

        [PageTitle("Contacter CoVoyage.net")]
        public ActionResult Contact()
        {
         //   ViewData["Title"] = "modifier le titre ici n'a aucun effet"; //l'attribut title prévaut!!
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [PageTitle("Contacter CoVoyage")]
        public ActionResult Contact(string nom, string email,string message,string sujet)
        {
            if(email.IsNullOrEmpty())
                this.ModelState.AddModelError("email", "Entrer une adresse email.");
            else
                 if(!CovCakeMailer.IsValidEmail(email))
                      this.ModelState.AddModelError("email", "L'adresse email est invalide!");
            if(message.IsNullOrEmpty())
                this.ModelState.AddModelError("message","Merci d'entrer un message.");
            if(sujet.IsNullOrEmpty())
                this.ModelState.AddModelError("sujet", "Merci de préciser un sujet.");
            if (nom.IsNullOrEmpty())
                this.ModelState.AddModelError("nom", "Merci de spécifier un nom");


            if(this.ModelState.IsValid)
            {
                try
                {
                   CovCakeMailer.SendContactMail(nom,email, sujet, message, this.Request.UserHostAddress);
                }
                catch(Exception ex)
                {
                    this.ModelState.AddModelError("_FORM", "Votre message n'a pu être envoyé. Vous pouvez toutefois nous contacter à l'adresse suivante : contact@covoyage.net");
                }
            }
            if(this.ModelState.IsValid)
            {
                this.ValidStateMessages.Add("Votre message a bien été envoyé merci !");
                this.ModelState.Clear();



            }
            return View();
        }

        [PageTitle("About Page")]
        public ActionResult About()
        {
            return View();

           // return View();
        }

    }
}
