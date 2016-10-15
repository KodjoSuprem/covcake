using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;

namespace covCake.Models
{
    public class AlerteViewData
    {
        public int? paysArrive { get; set; }
        public DateTime? dateDebut { get; set; }
        public DateTime? dateFin{ get; set; }
        public int? moisDebut { get; set; }
        public int? moisFin { get; set; }
        public int? nbJours { get; set; }
        public string villeArrive { get; set; }
        public PagedList<IAlerte> Alertes { get; set; }

        public AlerteViewData(IAlerte alerte)
        {
            this.Load(alerte);
        }
        public AlerteViewData()
        {

        }
        public void Load(IAlerte alert)
        {
            paysArrive = alert.IdPays;
            dateDebut = alert.DateDebutProjet;
            nbJours = alert.NbJours;
            villeArrive = alert.VilleArrive;
            if(alert.PeriodeDebut.HasValue)
                moisDebut = alert.PeriodeDebut.Value.Month;
            if (alert.PeriodeFin.HasValue)
                moisFin = alert.PeriodeFin.Value.Month;

        }
    }
}
