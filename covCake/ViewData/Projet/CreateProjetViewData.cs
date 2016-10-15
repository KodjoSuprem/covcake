using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;

namespace covCake.Models
{

    public class CreateProjetViewData
    {
        public IProjet Projet { get; set; }
        public int? idprojet;
        public int? projetid { get; set; }
        public string adresse { get; set; }
        public double? prixres { get; set; }
        public int? maxhotes { get; set; }
        public string typeres { get; set; }
        public string compagnie { get; set; }
        public string details { get; set; }
        public string modetrans { get; set; }
        public string numvol { get; set; }
        public double? prixtrans { get; set; }
        public string villeDepart { get; set; }
        public string villeArrive { get; set; }
        public string paysDepart { get; set; }
        public string paysArrive { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin { get; set; }
        public string commentaires { get; set; }
        public string heureDepart { get; set; }
        public int? nbjours { get; set; }
        public bool incertain { get; set; }
    }

    public class ResidenceViewData
    {
        public string adresse{get;set;}
        public string prixres{get;set;}
        public string maxhotes{get;set;}
        public string typeres{get;set;}
    }


    public class TransportViewData
    {
        public string compagnie{get;set;}
        public string details{get;set;}
        public string modetrans{get;set;}
        public string numvol{get;set;}
        public string prixtrans{ get; set; }

    }

    public class ProjetViewData //: cov_Projet
    {
        public string idprojet;
        public string villeDepart{get;set;}
        public string villeArrive{get;set;}
        public string paysDepart { get; set; }
        public string paysArrive{get;set;}
        public string dateDebut{get;set;}
        public string dateFin{get;set;}
        public string commentaires{get;set;}
        public string heureDepart { get; set; }
        public string nbjours { get; set; }
        public string incertain { get; set; }
        
        public ProjetViewData()
        {


        }

    }
}
