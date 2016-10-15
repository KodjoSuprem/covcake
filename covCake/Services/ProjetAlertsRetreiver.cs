using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;


namespace covCake.Services
{
    
    public class ProjetAlertsRetreiver
    {
        public struct ProjectAlertsResults
        {
            public int nbAlerts;
            public IUserProfile user;
            public IEnumerable<IProjet> projets;
        }

        /// <summary>
        /// Recupere les projet correspondants aux alertes de chaque utilisateur
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ProjectAlertsResults> RetrieveProjectsByUsers()
        {
            return RetrieveProjectsByUsers(false);
        }

        /// <summary>
        /// Recupere les projets correspondants aux alertes de chaque utilisateur
        /// </summary>
        /// <param name="UpdateAlerts">Si True Met a jour le champ DateDernierEnvoi à la date du jour</param>
        /// <returns></returns>
        public static IEnumerable<ProjectAlertsResults> RetrieveProjectsByUsers(bool UpdateAlerts)
        {
          DateTime NOW = DateTime.Now;
          List<ProjectAlertsResults> ProjectAlertsList = new List<ProjectAlertsResults>();
          var alertsByUsers = CovCake.DataProvider.AlerteDataAccess.GetAllAlertsByUsers();
          IQueryable<IProjet> projByAlerts = null;
         

          foreach (var userAlerts in alertsByUsers)
          {
             // var gfgf = userAlerts.Cast<IGrouping<IUserProfile, IAlerte>>();
              foreach (var alerte in userAlerts.Cast<IAlerte>())
              {
                  
                  var PROJETS = CovCake.DataProvider.ProjetDataAccess.GetAllProjets().Where(p => p.OwnerUserId != alerte.UserId); //ne renvoi pas ses propres projets
                  const int DATEDAYSOFFSET = 3;
                  const int NBJOURSOFFSET = 2;
                  //C'est parti!

                  if (alerte.IdPays != null)
                      PROJETS = PROJETS.ProjetsTo(alerte.IdPays.Value);

                  if (alerte.NbJours != null)
                      PROJETS = PROJETS.ProjetsNbJours(alerte.NbJours.Value,NBJOURSOFFSET);

                  if (alerte.DateDebutProjet != null)
                  {
                      DateTime debutProjet = alerte.DateDebutProjet.Value;
                      PROJETS = PROJETS.ProjetsDateDebutBetween(debutProjet.AddDays(-DATEDAYSOFFSET), debutProjet.AddDays(DATEDAYSOFFSET));
                  }

                  if (alerte.PeriodeDebut != null &&  alerte.PeriodeFin != null)
                  {
                      DateTime periodeDebut = alerte.PeriodeDebut.Value;
                      DateTime periodeFin = alerte.PeriodeFin.Value;

                      PROJETS = PROJETS.ProjetsDateDebutBetween(periodeDebut, periodeFin);
                   }

                
                  //FIXME: Rechercher pk sa plante avec Contains sa craind
                  if (alerte.VilleArrive.IsNullOrEmpty() == false)
                      PROJETS = PROJETS.Where(p => (p.VilleArrive != "" && p.VilleArrive != null) && p.VilleArrive.ToLower() == alerte.VilleArrive.ToLower());// .Where(p=> alerte.VilleArrive.Contains(p.VilleArrive)); //va [pas ][MAJ 24/10] péter :)
          
                  //System.Diagnostics.Debug.WriteLine(PROJETS.Provider.ToString());//.ToString());

                  //DATE DERNIERS ENVOI
                  PROJETS = PROJETS.Where(p => p.DateCreation >= alerte.DateDernierEnvoi);


                  if (projByAlerts == null)
                      projByAlerts = PROJETS;
                  else
                      projByAlerts = projByAlerts.Concat(PROJETS);

                  
                  if (UpdateAlerts)
                      alerte.DateDernierEnvoi = NOW;
              }

              //Array Unique
              
              //ADD NEW PROJECT SUGGESTIONS
             // System.Diagnostics.Debug.WriteLine("Besfore going");

              projByAlerts = projByAlerts.Distinct();
           
              ProjectAlertsResults rez = new ProjectAlertsResults()
              {
                  nbAlerts =userAlerts.Count(),
                  user = userAlerts.Key,
                  projets = projByAlerts
              };

              ProjectAlertsList.Add(rez);
              projByAlerts = null;
          }
          
          return ProjectAlertsList;
        }
    }
}
