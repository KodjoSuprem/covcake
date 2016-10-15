using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace covCake.DataAccess
{
    [DebuggerStepThrough]
    public class TransportDataAccess
    {
        private covCakeDataContext _dataContext;


        
        public TransportDataAccess(covCakeDataContext dataBase)
        {
            
            _dataContext = dataBase;
        }

        public TransportDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }


        public ITransport CreateTransport()
        {
            return new cov_Transport() as ITransport;
        }

        public void InsertTransport(ITransport trans)
        {

            this._dataContext.cov_Transports.InsertOnSubmit((cov_Transport)trans);
            this._dataContext.SubmitChanges();
        }

        public ITransport GetTransport(int idTrans)
        {
            return (from T in _dataContext.cov_Transports
                    where T.IdTransport == idTrans
                    select T).SingleOrDefault();
        }

        public void Remove(int idTrans)
        {
            ITransport trans = GetTransport(idTrans);
            this._dataContext.cov_Transports.DeleteOnSubmit((cov_Transport)trans);

            this._dataContext.SubmitChanges();
        }

        public void Remove(ITransport trans)
        {
            this._dataContext.cov_Transports.DeleteOnSubmit((cov_Transport)trans);
            this._dataContext.SubmitChanges();
        }
    }
}
