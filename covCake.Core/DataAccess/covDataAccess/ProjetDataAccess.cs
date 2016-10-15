using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace covCake.DataAccess
{
    [DebuggerStepThrough]
    public class ProjetDataAccess //: IDataAccessLayer
    {

        private covCakeDataContext _dataContext;

        public ProjetDataAccess(covCakeDataContext dataBase)
        {
            
            _dataContext = dataBase;
        }

        public ProjetDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }


        public void SubmitChanges()
        {
            this._dataContext.SubmitChanges();
        }

        #region IDataAccessLayer Members

     

        public void Insert(ICovEntity proj)
        {
            this._dataContext.cov_Projets.InsertOnSubmit((cov_Projet)proj);
            this._dataContext.SubmitChanges();
        }

        #endregion

        #region Create
        public  IProjet Create()
        {
            return new cov_Projet();
        }
        #endregion

        #region Insert
        public void InsertProjet(IProjet proj)
        {
            this._dataContext.cov_Projets.InsertOnSubmit((cov_Projet)proj);
            this._dataContext.SubmitChanges();
        }
        #endregion



        #region Remove
        public void Remove(int ProjetId)
        {

            IProjet proj = this.GetProjet(ProjetId);
            this._dataContext.cov_Projets.DeleteOnSubmit((cov_Projet)proj);
            this._dataContext.SubmitChanges();

        }
        public void Remove(IProjet proj)
        {
            this._dataContext.cov_Projets.DeleteOnSubmit((cov_Projet)proj);
            this._dataContext.SubmitChanges();

        }
        #endregion

        #region Queries on Projet

        public IProjet GetProjet(int ProjetId)
        {
            return (from P in _dataContext.cov_Projets
                    where P.IdProjet == ProjetId
                    select P).SingleOrDefault();
        }


        public IQueryable<IProjet> GetAllProjetsNonRealise()
        {
            return (from P in _dataContext.cov_Projets
                    where P.Realise == false
                    orderby P.DateDebut
                    select P).Cast<IProjet>();

        }

        public IQueryable<IProjet> GetAllProjets()
        {
            return (from P in _dataContext.cov_Projets
                    orderby P.DateDebut
                    select P).Cast<IProjet>();

        }

        public IQueryable<IProjet> GetAllProjetsOutdated()
        {
            return (from P in _dataContext.cov_Projets
                    where P.DateFin < DateTime.Now
                    select P).Cast<IProjet>();
        }

       
        public IQueryable<IProjet> GetAllProjetsRealise()
        {
            return (from P in _dataContext.cov_Projets
                    where P.Realise == true
                    select P).Cast<IProjet>(); 
        }



        public IQueryable<IProjet> GetAllProjetBetween(DateTime debut, DateTime fin)
        {
            return (from P in _dataContext.cov_Projets
                    where P.DateDebut >= debut && P.DateFin <= fin
                    select P).Cast<IProjet>(); 
        }

       public IQueryable<IProjet> GetAllProjetOf(Guid UserID)
        {
            return (from P in _dataContext.cov_Projets
                    where P.OwnerUserId == UserID
                    select P).Cast<IProjet>(); 
        }

       public IQueryable<IProjet> GetAllProjetTo(int PaysId)
        {
            return (from P in _dataContext.cov_Projets
                    where P.IdPaysArrive == PaysId
                    select P).Cast<IProjet>();
        }
       public IQueryable<IProjet> GetAllProjetTo(string PaysLibelle)
        {
            return (from PJ in _dataContext.cov_Projets
                    join PY in _dataContext.ref_Pays
                    on PJ.IdPaysArrive equals PY.IdPays
                    where PY.LibellePays.ToUpper().Contains(PaysLibelle.ToUpper())
                    select PJ).Cast<IProjet>();
        }
       public IQueryable<IProjet> GetAllProjetFrom(int PaysId)
        {
            return (from P in _dataContext.cov_Projets
                    where P.IdPaysDepart == PaysId
                    select P).Cast<IProjet>();
        }

       public IQueryable<IProjet> GetAllProjetFrom(string PaysLibelle)
        {
            return (from PJ in _dataContext.cov_Projets
                    join PY in _dataContext.ref_Pays
                    on PJ.IdPaysDepart equals PY.IdPays
                    where PY.LibellePays.ToUpper().Contains(PaysLibelle.ToUpper())
                    select PJ).Cast<IProjet>();
        }


       public IQueryable<IProjet> SameDestinationAs(IQueryable<IProjet> projets)
       {
           return (from P in _dataContext.cov_Projets
                   from P2 in projets
                   where P.IdPaysArrive == P2.IdPaysArrive
                   select P).Cast<IProjet>();
       }


        #endregion

      
    }
    public static class ProjetDataAccessFilters
    {

        public static IQueryable<IProjet> ProjetsNbJours(this IQueryable<IProjet> query, int nbJours)
        {

            return (from P in query
                    where P.NbJours == nbJours
                    select P);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="nbJours"></param>
        /// <param name="offset">ecart entre </param>
        /// <returns></returns>
        public static IQueryable<IProjet> ProjetsNbJours(this IQueryable<IProjet> query, int nbJours, int offset)
        {
            if (!(nbJours > offset))
                throw new Exception("nbJours ne peut être inferieur ou égale à offset");

            return (from P in query
                    where P.NbJours >= nbJours - offset  && P.NbJours <= nbJours + offset
                    select P);
        }

        public static IQueryable<IProjet> ProjetsNonOutdated(this IQueryable<IProjet> query)
        {

            return (from P in query
                    where P.DateDebut > DateTime.Now
                    select P);
        }
        public static IQueryable<IProjet> ProjetsPostedSince(this IQueryable<IProjet> query, DateTime date)
        {

            return (from P in query
                    where P.DateCreation >= date
                    select P);
        }
        public static IQueryable<IProjet> ProjetsNonRealises(this IQueryable<IProjet> query)
        {
            return (from P in query
                    where P.Realise == false
                    select P);
        }

        public static IQueryable<IProjet> ProjetsCreeDepuis(this IQueryable<IProjet> query, DateTime dateCreation)
        {
            //  DataProvider data = new DataProvider();
            return (from P in query
                    where P.DateCreation >= dateCreation
                    select P);
        }

        //TODO: Projet réalisés = projets créés ou projets créés + participé
        public static IQueryable<IProjet> ProjetRealises(this IQueryable<IProjet> query)
        {
        //  DataProvider data = new DataProvider();
            return (from P in query
                    where P.Realise == true
                    select P);
        }

        /// <summary>
        /// Depart apres(ou =) à debut et Retour avant(ou =) à fin
        /// </summary>
        /// <param name="query"></param>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        public static IQueryable<IProjet> ProjetsBetween(this IQueryable<IProjet> query, DateTime debut, DateTime fin)
        {
            return (from P in query
                    where P.DateDebut >= debut && P.DateFin <= fin
                    select P);
        }

        public static IQueryable<IProjet> ProjetsDateDebutBetween(this IQueryable<IProjet> query, DateTime debut, DateTime fin)
        {
            return (from P in query
                    where P.DateDebut >= debut && P.DateDebut <= fin
                    select P);
        }

        public static IQueryable<IProjet> ProjetsDateFinBetween(this IQueryable<IProjet> query, DateTime debut, DateTime fin)
        {
            return (from P in query
                    where P.DateFin >= debut && P.DateFin <= fin
                    select P);
        }
        public static IQueryable<IProjet> ProjetsFrom(this IQueryable<IProjet> query, int PaysId)
        {
            return (from P in query
                    where P.IdPaysDepart == PaysId
                    select P);
        }

        public static IQueryable<IProjet> ProjetsTo(this IQueryable<IProjet> query, int PaysId)
        {
            return (from P in query
                    where P.IdPaysArrive == PaysId
                    select P);
        }

        public static IQueryable<IProjet> SameDestinationAs(this IQueryable<IProjet> query, IQueryable<IProjet> projets)
        {
            return (from P in query
                    from P2 in projets //solution de contournement
                    where P.IdPaysArrive == P2.IdPaysArrive
                    select P);
        }
        /*
        public static IQueryable<IProjet> Except(this IQueryable<IProjet> query, IQueryable<IProjet> projets)
        {
            return (from P in query
                    from P2 in projets //solution de contournement
                    where P.IdProjet != P2.IdProjet
                    select P);
        }
         * */
        public static IQueryable<IProjet> SameDateDebutAs(this IQueryable<IProjet> query, IQueryable<IProjet> projets)
        {
            return (from P in query
                    from P2 in projets
                    where P.DateDebut == P2.DateDebut
                    select P);
        }
        public static IQueryable<IProjet> SameDateFinAs(this IQueryable<IProjet> query, IQueryable<IProjet> projets)
        {
            return (from P in query
                    from P2 in projets
                    where P.DateFin == P2.DateFin
                    select P);
        }

    }
}
