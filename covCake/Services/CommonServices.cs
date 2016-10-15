using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Collections.Specialized;

namespace covCake.Services
{
    public struct DateNaissanceSelector
    {
        public  SelectList Jours{get;  set;}
        public  SelectList Mois{get;  set;}
        public  SelectList Annees{get;  set;}


    }
    public struct Jour
    {
        public string Nom { get; set; }
        public string No{get; set;}
    }

    public struct Mois
    {
        public string Nom { get; set; }
        public string No { get; set; }
    }




    public class CovCakeServices
    {
        public static Random  rand = new Random();
        public static string GenerateAlphaNumPass(int length)
        {
            string pass = "";
            for (int i = 0; i < length; ++i)
            {
                switch (rand.Next(1, 4))
                {
                    case 1: pass += (char)rand.Next('a', 'z'+1); break;
                    case 2: pass += (char)rand.Next('A', 'Z'+1); break;
                    case 3: pass += (char)rand.Next('1', '9'+1); break;
                }
            }


            return pass;
        }

        private static string[] _listeMois = { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre" };
        public  static string [] ListeMois{get{return _listeMois;}}
        public static DateNaissanceSelector GetDateNaissSelector(string selectedJour, string selectedMois, string selectedAnnee)
        {
            DateNaissanceSelector sel = new DateNaissanceSelector();
            List<Jour> jours = new List<Jour>(31);
            List<int> annees = new List<int>(90);
            List<Mois> mois = new List<Mois>(12);
            for(int j = 1; j <= jours.Capacity; ++j)
            {
                jours.Add(new Jour() { No = j.ToString(), Nom = j.ToString() });
            }
            for(int a = 10; a < annees.Capacity; ++a)
            {
                annees.Add(1900 + a);
            }
            for(int m = 1; m <= mois.Capacity; m++)
            {
                mois.Add(new Mois() { Nom = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m).ToTitleCase(), No = m.ToString() });
            }

            sel.Jours = new SelectList(jours, "No", "Nom", selectedJour);
            sel.Annees = new SelectList(annees, selectedAnnee);
            sel.Mois = new SelectList(mois, "No", "Nom",selectedMois);
            return sel;

        }
        public static DateNaissanceSelector GetDateNaissSelector()
        {
            return GetDateNaissSelector("01", "01", "90");
        }
    }
}
