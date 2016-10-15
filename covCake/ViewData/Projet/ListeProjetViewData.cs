using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using covCake.DataAccess;

namespace covCake.Models
{
    public class ListeProjetViewData
    {
        public SearchProjetParams SearchParams { get; set; }
        public bool IsSearchResults {get;set;}
        public bool SubstituteListe { get; set; }
        public PagedList<IProjet> ListeProjets
        {
            get;
            set;

        }
    }
}
