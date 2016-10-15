using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public class AbonnementProjetDataAccess
    {
          private covCakeDataContext _dataContext;

        public AbonnementProjetDataAccess(covCakeDataContext dataBase)
        {
            
            _dataContext = dataBase;
        }

        public AbonnementProjetDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }


        public IAbonnementProjet CreateAbonnement()
        {
            return new cov_AbonnementProjet() as IAbonnementProjet ;
        }

        public void DeleteAbonnement(IAbonnementProjet abo)
        {
            _dataContext.cov_AbonnementProjets.DeleteOnSubmit((cov_AbonnementProjet)abo);
            _dataContext.SubmitChanges();
        }


        public void DeleteAbonnement(Guid UserId, int IdProjet)
        {

            this.DeleteAbonnement(this.GetAbonnement(UserId, IdProjet));
        }
        /// <summary>
        /// Obtient l'abonnement pour l'user et le projet spécifié
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IdProjet"></param>
        /// <returns></returns>
        public IAbonnementProjet GetAbonnement(Guid UserId, int IdProjet)
        {
            return (from A in _dataContext.cov_AbonnementProjets
                    where A.IdProjet == IdProjet && A.UserId == UserId
                    select A).SingleOrDefault();
        }
        
        public IQueryable<IAbonnementProjet> GetAllAbonnements()
        {
            return (from A in _dataContext.cov_AbonnementProjets
                    select A).Cast<IAbonnementProjet>();
        }
        
        public IQueryable<IAbonnementProjet> GetAllAbonnements(int IdProjet)
        {
            return (from A in _dataContext.cov_AbonnementProjets
                    where A.IdProjet == IdProjet
                    select A).Cast<IAbonnementProjet>();
        }

        public IQueryable<IAbonnementProjet> GetAllAbonnementsOf(Guid UserId)
        {
            return (from A in _dataContext.cov_AbonnementProjets
                    where A.UserId == UserId
                    select A).Cast<IAbonnementProjet>();
        }


        public IQueryable<IAbonnementProjet> GetAllAbonnementsConcerning(IQueryable<IProjet> projets)
        {
            return (from A in _dataContext.cov_AbonnementProjets
                    from P in projets
                    where A.IdProjet == P.IdProjet
                    select A).Cast<IAbonnementProjet>();
        }

        /// <summary>
        /// Projets auquels userId et mateUserId sont abonnés
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mateUserId"></param>
        /// <returns></returns>
        public IQueryable<IProjet> GetAllSharedProjectsSimple(Guid userId, Guid mateUserId)
        {

            return (from A in _dataContext.cov_AbonnementProjets
                    from A2 in _dataContext.cov_AbonnementProjets                
                    where (A.UserId == userId
                         && A2.UserId == mateUserId
                         && A.IdProjet == A2.IdProjet)
                    select A.cov_Projet).Cast<IProjet>();
        }


        /// <summary>
        /// Tout les Abonnements qui concernent les projets ou userId participe 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<IAbonnementProjet> GetAllSharedAbonnementSimple(Guid userId)
        {

            return (from A in _dataContext.cov_AbonnementProjets
                    from A2 in _dataContext.cov_AbonnementProjets
                    where (A.UserId == userId && A.IdProjet == A2.IdProjet)
                    select A2).Cast<IAbonnementProjet>();
        }


        /// <summary>
        /// Projets auquels userId et mateUserId sont abonnés dont ceux créés par userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mateUserId"></param>
        /// <returns></returns>èy-
        public IQueryable<IProjet> GetAllSharedProjects(Guid userId, Guid mateUserId)
        {

            return (from A in _dataContext.cov_AbonnementProjets
                    from A2 in _dataContext.cov_AbonnementProjets
                    from P in _dataContext.cov_Projets
                    //from A3 in P.cov_AbonnementProjets
                   // from A3 in _dataContext.cov_AbonnementProjets
                    where (A.UserId == userId
                         && A2.UserId == mateUserId
                         && A.IdProjet == A2.IdProjet)
                         ||
                         (P.OwnerUserId == userId
                         && A.IdProjet == P.IdProjet
                         && A.UserId == mateUserId)
                    select A.cov_Projet).Distinct().Cast<IProjet>();


        }


    }


    public static class AbonnementProjetExtensions
    {
        public static AbonnementProjetDataAccess AbonnementProjetDataAccess = new AbonnementProjetDataAccess();

        public static bool HaveSubscribed(this IQueryable<IAbonnementProjet> query, Guid userId)
        {
            return (query.Where(s => s.UserId == userId).FirstOrDefault() != null);
                      
        }


        public static IQueryable<IAbonnementProjet> UserAbonnes(this IQueryable<IProjet> query)
        {
            //return AbonnementProjetDataAccess.GetAllAbonnementsConcerning(query);
            int count = 0;
            if (query.FirstOrDefault() == null) return new List<IAbonnementProjet>().AsQueryable<IAbonnementProjet>();
            IQueryable<IAbonnementProjet> abonnes = query.First().UserAbonnes;
            foreach (IProjet item in query)
            {
                if(count == 0) continue;
                abonnes = abonnes.Concat(item.UserAbonnes);
                count++;
            }
            return abonnes;
        }
       
       
        public static IQueryable<IAbonnementProjet> GetAllAbonnementTo(this IQueryable<IAbonnementProjet> query, IProjet projet)
        {
            return (from A in query
                    where A.ProjetId == projet.IdProjet
                    select A).Cast<IAbonnementProjet>();
        }
        public static IQueryable<IAbonnementProjet> GetAllAbonnementTo(this IQueryable<IAbonnementProjet> query, int projetId)
        {
            return (from A in query
                    where A.ProjetId == projetId
                    select A).Cast<IAbonnementProjet>();
        }

        public static IQueryable<IAbonnementProjet> GetAllAbonnementsConcerning(this IQueryable<IAbonnementProjet> query, IQueryable<IProjet> projets)
        {
            return (from A in query
                    from P in projets
                    where A.ProjetId == P.IdProjet
                    select A);//.Cast<IAbonnementProjet>();

        }

        public static IQueryable<IUserProfile> GetAllUserProfiles(this IQueryable<IAbonnementProjet> query)
        {
            return (from A in query
                    select A.UserProfile).Cast<IUserProfile>();
        }
        public static IQueryable<IAbonnementProjet> GetAllAbonnementSince(this IQueryable<IAbonnementProjet> query, DateTime dateAbonnement)
        {
            return (from A in query
                    where A.DateAbonnement >= dateAbonnement
                    select A);
        }
    }
}
