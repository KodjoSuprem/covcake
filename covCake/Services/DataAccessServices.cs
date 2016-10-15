using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using covCake.DataAccess;
namespace covCake.Services
{

   
    public class ResidenceServices
    {
        //private static string[] type ;//= { "Hotel", "Camping", "Auberge", "Amis","Chez Moi", "Autre" }; 

        public static SelectList ListeType()
        {
            return ListeType("");
        }
        public static SelectList ListeType(string selected)
        {
            List<string> l = cov_Residence.TYPESRESIDENCE.ToList();
            for(int i = 0; i < l.Count; i++)
                l[i] = cov_Residence.TYPESRESIDENCE[i].ToTitleCase();

            if (selected.IsNullOrEmpty())
                return new SelectList(l);
            return new SelectList(l, selected);
        }

        public static SelectList ListeTypeDefaultMessage(string defaultMessage)
        {
            List<string> l =  cov_Residence.TYPESRESIDENCE.ToList();
            for(int i = 0; i < l.Count; i++)
                l[i] = cov_Residence.TYPESRESIDENCE[i].ToTitleCase();
          
            l.Insert(0,defaultMessage);
            return new SelectList(l, defaultMessage);
        }
    }

    public class TransportServices
    {
       // private static string[] modes = { "Avion", "Train", "Bateau", "Voiture", "Autre" };
        public static SelectList ListeModes()
        {
            return ListeModes("");
        }

        public static SelectList ListeModes(string selected)
        {
            List<string> l = cov_Transport.MODETRANSPORT.ToList();
            for(int i = 0; i < l.Count; i++)
                l[i] = cov_Transport.MODETRANSPORT[i].ToTitleCase();
            if(selected.IsNullOrEmpty())
                return new SelectList(l);
            return new SelectList(l, selected);
        }

        public static SelectList ListeModesDefaultMessage(string defaultMessage)
        {
            List<string> l = cov_Transport.MODETRANSPORT.ToList();
            for(int i = 0; i < l.Count; i++)
                l[i] = cov_Transport.MODETRANSPORT[i].ToTitleCase();

            l.Insert(0,defaultMessage);
            return new SelectList(l, defaultMessage);
        }
       
    }


    public class PaysServices
    {
        private static CovCakeData dataProvider = CovCake.DataProvider; // new CovCakeData();
       private static string[] commonPays = { "Allemagne", "Royaume-uni","Martinique","Guadeloupe","Belgique", "France" };
        //TODO: compléter liste des pays de destination 
       private static string[] commonDestPays = { "France", "Royaume-uni", "Australie", "Chine", "Japon", "Inde", "Bénin", "Cameroun" };
     


       public static SelectList PaysDestination()
       {
           return ListePays("40", commonDestPays);
       }

      
       public static SelectList ListePays(string selectedPaysId, IList<string> listepays)
       {
           //IList<string> allPays = dataProvider.PaysDataAccess.GetAllPaysString();
           IList<IPays> allPaysE = (IList<IPays>)CovCake.UnCache("AllPays");
           if(allPaysE == null)
           {
               allPaysE = dataProvider.PaysDataAccess.GetAllPays().ToList();
               int cacheTime = CovCakeConfiguration.DefaultDataCacheExpiration;
               CovCake.Cache("AllPays", allPaysE, DateTime.Now.AddHours(cacheTime));   
              // HttpRuntime.Cache.Insert("AllPays", allPaysE, null, DateTime.Now.AddHours(cacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
           }
           int selPaysId =77;
           if (!selectedPaysId.IsNullOrEmpty())
               int.TryParse(selectedPaysId, out selPaysId);//france
           
           foreach(string item in listepays)
           {
               IPays insertP = allPaysE.Single(p=> p.LibellePays == item);
               allPaysE.Remove(insertP);
               allPaysE.Insert(0,insertP);
           }
          // allPays.Insert(0, "------------------------");
           if(selectedPaysId.IsNullOrEmpty())
               return new SelectList(allPaysE, "IdPays", "LibellePays");// selectedP);
           return new SelectList(allPaysE, "IdPays", "LibellePays", selPaysId);// selectedP);
         //  return new SelectList(allPays, selectedPaysId);
       }
    
       public static SelectList ListePays(string selectedId)
       {
           return ListePays(selectedId, commonPays);
       }
        public static SelectList ListePays()
        {
            //france par defaut
            return ListePays(null,commonPays);
        }

        public static IEnumerable<string> ListePaysString2()
        {
            //france par defaut
            return ListePays().Items.OfType<IPays>().Select(p => p.LibellePays);
        }


        public static IQueryable<string> ListePaysString()
        {
            IQueryable<string> allLibelle = (IQueryable<string>)HttpRuntime.Cache["LibellePays"];
            if(allLibelle == null)
            {
                allLibelle = dataProvider.PaysDataAccess.GetAllPays().Select(p => p.LibellePays);
                int cacheTime = CovCakeConfiguration.DefaultDataCacheExpiration;
                HttpRuntime.Cache.Insert("LibellePays", allLibelle, null, DateTime.Now.AddHours(cacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
            }

            return allLibelle;
        }
    }

    public class DepartementService
    {
        private static CovCakeData dataProvider = CovCake.DataProvider;

        public static SelectList ListeDept(string selectedDept)
        {
            IList<IDepartement> listDep = (IList<IDepartement>)HttpRuntime.Cache["ListDep"];
            if(listDep == null)
            {
                listDep = dataProvider.DepartementDataAcess.GetAllDepartments().ToList();
                int cacheTime = CovCakeConfiguration.DefaultDataCacheExpiration;
                HttpRuntime.Cache.Insert("ListDep", listDep, null, DateTime.Now.AddHours(cacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
            }
           // HttpContext.Current.Cache["ListDep"] = listDep;

 
            return new SelectList(listDep, "NumDept", "NomDept", selectedDept);

        }

        public static SelectList ListeDept()
        {
            return ListeDept("01");
            
        }
    }
}
