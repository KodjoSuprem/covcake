using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public interface IPays : ICovEntity
    {

         int IdPays { get;}

         string LibellePays { get; }

         string LibelleEngPays{get;}

        /// <summary>
        /// Code pays sur 3charsa
        /// </summary>
         string CodePays{get;}

        /// <summary>
        /// Code pays sur 2 char
        /// </summary>
         string CodePays2{get;}

         string CapitalePaysEng {get;}
    }
}
