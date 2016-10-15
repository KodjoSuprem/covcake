using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public partial class ref_Pays : IPays
    {


        #region IPays Members

         int IPays.IdPays
        {
            get
            {
                return IdPays;
            }
         
        }

         string IPays.LibellePays
        {
            get
            {
                return LibellePays;
            }
          
        }

         string IPays.LibelleEngPays
        {
            get
            {
                return LibelleEngPays;
            }
          
        }

         string IPays.CodePays
        {
            get
            {
                return CodePays;
            }
        
        }

         string IPays.CodePays2
        {
            get
            {
                return CodePays2;
            }
           
        }

         string IPays.CapitalePaysEng
        {
            get
            {
                return CapitalePays;
            }
           
        }

        #endregion
    }

    public static class PaysExtension
    {
        public static string Libelle(this IPays pays)
        {
            return (pays != null) ? pays.LibellePays : "";
        }

        

    }


}
