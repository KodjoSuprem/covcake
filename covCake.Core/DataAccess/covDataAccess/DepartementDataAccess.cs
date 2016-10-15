using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public class DepartementDataAccess
    {
        private covCakeDataContext _dataContext;

        public DepartementDataAccess(covCakeDataContext dataBase)
        {
            
            _dataContext = dataBase;
        }

        public DepartementDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }

        public IQueryable<IDepartement> GetAllDepartments()
        {
            return (from D in _dataContext.ref_Departements
                    select D).Cast<IDepartement>();
        }

        public IDepartement GetDept(string numdept)
        {
            return (from D in _dataContext.ref_Departements
                    where D.NumDept == numdept
                    select D).SingleOrDefault();
        }
    }
}
