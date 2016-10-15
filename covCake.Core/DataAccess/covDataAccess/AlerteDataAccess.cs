using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public class AlerteDataAccess
    {
       
         private covCakeDataContext _dataContext;

        public AlerteDataAccess(covCakeDataContext dataBase)
        {
            _dataContext = dataBase;
        }

        public AlerteDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }


        public void SubmitChanges()
        {
            this._dataContext.SubmitChanges();
        }

        #region Create
        public IAlerte Create()
        {
            return new cov_Alerte();
        }
        #endregion

        #region Insert
        public void InsertAlerte(IAlerte alert)
        {
            this._dataContext.cov_Alertes.InsertOnSubmit((cov_Alerte)alert);
            this._dataContext.SubmitChanges();
        }
        #endregion



        public IAlerte GetAlert(int alertId)
        {
            return (from A in this._dataContext.cov_Alertes
                    where A.IdAlerte == alertId
                    select A).SingleOrDefault();
        }

        public void Delete(IAlerte alert)
        {
            _dataContext.cov_Alertes.DeleteOnSubmit((cov_Alerte)alert);
            _dataContext.SubmitChanges();
        }

        public void DeleteAlertes(IEnumerable<IAlerte> alertesFiltered)
        {
            _dataContext.cov_Alertes.DeleteAllOnSubmit(alertesFiltered.Cast<cov_Alerte>());
            _dataContext.SubmitChanges();
        }

        /*
        public IEnumerable<IProjet> GetAllProjetsFor(int alertId)
        {
            return (from A in _dataContext.cov_Alertes
                    from P in _dataContext.cov_Projets
                    where A.IdAlerte == alertId
                    &&    A.IdPays
                    select A);
        }
        */

        public IQueryable<IGrouping<cov_UserProfile,cov_Alerte>> GetAllAlertsByUsers()
        {

            //var test = (from A in _dataContext.cov_Alertes
            //            from U in _dataContext.cov_UserProfiles
            //            where A.UserId == U.UserId
            //            group A by U);

            //string oo = test.GetType().ToString();

            //
            /*
             *
             * Dim q = From p In db.Products _
            Group p By p.CategoryID Into Group _
            Select Group

             * */
            return (from A in _dataContext.cov_Alertes
                    from U in _dataContext.cov_UserProfiles
                    where A.UserId == U.UserId
                    group A by U);//into GA
                                    //select GA) as IQueryable<IGrouping<IUserProfile, IAlerte>>
        }

        public IQueryable<IAlerte> GetAllAlertsFor(Guid userID)
        {
            return (from A in _dataContext.cov_Alertes
                    where A.UserId == userID
                    select A).Cast<IAlerte>();
        }

       
    }
}
