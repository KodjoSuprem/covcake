using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;

namespace covCake.Models
{
    public class MonCompteViewData
    {
        public IUserProfile UserProfile { get; set; }
        public IResidence FakeResidence { get; set; }
        public ITransport FakeTransport { get; set; }

        public PagedList<IProjet> UserProjets { get; set; }
        public PagedList<IAbonnementProjet> UserAbonnements { get; set; }
        public PagedList<IUserProfile> UserAbonnesProfiles { get; set; }
        public PagedList<IAbonnementProjet> UserAbonnes { get; set; }
        public DateTime LastNewsDate { get; set; }

        public int NewsDaysOffset { get; set; }
        /// <summary>
        /// Méssages non lus récents
        /// </summary>
        public IQueryable<IMessagePrive> NewsNewMessages { get; set; }

         /// <summary>
         /// Abonnement récents
         /// </summary>
         public IQueryable<IUserProfile> NewsAbonnes { get; set; }

        
        /// <summary>
        /// projets similaires récents
        /// </summary>
         public IQueryable<IProjet> NewsSimilarProjets { get; set; }

         /// <summary>
         /// Covoyageurs récents
         /// </summary>
         public IQueryable<IAbonnementProjet> NewsMates { get; set; }

         public MonCompteViewData(CovCakeData data)
         {
             this.FakeResidence = data.ResidenceDataAccess.CreateResidence();
             this.FakeResidence.Adresse = "Non Renseignée";
             this.FakeResidence.Prix = 0d;
             this.FakeResidence.Type = "Non Renseignée";

             this.FakeTransport = data.TransportDataAccess.CreateTransport();
             this.FakeTransport.Compagnie = "Non Renseigné";
             this.FakeTransport.ModeTransport = "Non Renseigné";
             this.FakeTransport.PrixAR = 0d;
             this.FakeTransport.NumVol = "";
         }
         public MonCompteViewData()
         {
             /*
             this.FakeResidence = data.ResidenceDataAccess.CreateResidence();
             this.FakeResidence.Adresse = "Non Renseignée";
             this.FakeResidence.Prix = 0d;
             this.FakeResidence.Type = "Non Renseignée";

             this.FakeTransport = data.TransportDataAccess.CreateTransport();
             this.FakeTransport.Compagnie = "Non Renseigné";
             this.FakeTransport.ModeTransport = "Non Renseigné";
             this.FakeTransport.PrixAR = 0d;
             this.FakeTransport.NumVol = "";p*/
         }

    }
}
