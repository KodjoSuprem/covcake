using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using covCake.DataAccess;

namespace covCake.Models
{
    public class UserInfosViewData
    {
        public const int YEAR_OFFSET = 1900;
        public string nom { get; set; }
        public string prenom { get; set; }
        public string age_jour { get; set; }//= form["age_jour"];
        public string age_mois { get; set; }//= form["age_mois"];
        public string age_annee { get; set; }//= form["sexe"];
        public string sexe { get; set; }//= form["age_annee"];
        public string numdept { get; set; }//= form["numdept"];
        public string ville{ get; set; }// = form["ville"];
        public string password { get; set; }//= form["password"];
        public string confirmPassword { get; set; }//= form["confirmPassword"];
        public string email {get;set;}//= form["email"].ToLower();
        public string pays { get; set; }

        public UserInfosViewData()
        {
        }

        public UserInfosViewData(IUserProfile user)
        {
            this.nom = user.Nom;
            this.prenom = user.Prenom;
            this.sexe = user.Sexe.ToString(); //= form["age_annee"];
            this.numdept = user.NumDepartement; //= form["numdept"];
            this.ville=user.Ville; // = form["ville"];
            this.age_annee = (user.DateNaissance.Year - YEAR_OFFSET).ToString();
            this.age_mois = user.DateNaissance.Month.ToString();
            this.age_jour = user.DateNaissance.Day.ToString();
            this.email = user.Email;
        
            //this.pays = use
        }

    
        public IUserProfile UpdateUserProfile(IUserProfile user,CovCakeData Data)
        {
            user.Nom = this.nom.Trim().ToTitleCase();
            user.Prenom = this.prenom.Trim().ToTitleCase();
            user.Sexe = bool.Parse(this.sexe);
            user.Ville = this.ville.Trim().ToTitleCase();
            user.DateNaissance = new DateTime(int.Parse(this.age_annee) + YEAR_OFFSET, int.Parse(this.age_mois), int.Parse(this.age_jour));
            //user.Departement. = null;
           
               IDepartement dept = Data.DepartementDataAcess.GetDept(this.numdept);
               user.Departement = dept;
       
            //TODO: Enregistrer PAYS et MailPulique? en base pour l'utilisateur
            //user.PaysId = this.pays;
            //user.EmailPublique = bool.Parse(this.PublicMail);
            return user;
        }
    }
}
