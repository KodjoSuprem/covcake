using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public class ResidenceDataAccess
    {
        private covCakeDataContext _dataContext;

        public ResidenceDataAccess(covCakeDataContext dataBase)
        {
            
            _dataContext = dataBase;
        }

        public ResidenceDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }


        public IResidence CreateResidence()
        {
            return new cov_Residence() as IResidence;
        }


        public IResidence GetResidence(int idResidence)
        {
            return (from R in _dataContext.cov_Residences
                    where R.IdResidence == idResidence
                    select R).SingleOrDefault();
        }


        public void MarkForRemove(int idResidence)
        {
            IResidence res = this.GetResidence(idResidence);
            this._dataContext.cov_Residences.DeleteOnSubmit((cov_Residence)res);
        }
       
        public void Remove(int idResidence)
        {
            IResidence res = this.GetResidence(idResidence);
            this._dataContext.cov_Residences.DeleteOnSubmit((cov_Residence)res);
            this._dataContext.SubmitChanges();
        }


        public void Remove(IResidence res)
        {
            this._dataContext.cov_Residences.DeleteOnSubmit((cov_Residence)res);
            this._dataContext.SubmitChanges();
        }
    }
}
