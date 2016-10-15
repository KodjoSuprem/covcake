using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.Models
{
    public class SearchProjetParams
    {
        public int? paysArriveId  { get; set; }
        public string paysArrive { get; set; }
        public DateTime? dateDebut { get; set; }
        public int? nbJours { get; set; }
        public int? moisPeriodeDeb { get; set; }
        public int? moisPeriodeFin { get; set; }
    }
}
