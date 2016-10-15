using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public class PaysDataAccess : IPaysDataAccess
    {
        private covCakeDataContext _dataContext;

        public PaysDataAccess(covCakeDataContext dataBase)
        {
            
            _dataContext = dataBase;
        }

        public PaysDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }

        public IQueryable<IPays> GetAllPays()
        {
            return (from P in _dataContext.ref_Pays
                    select P).Cast<IPays>();

        }
        public IList<string> GetAllPaysString()
        {
            return (from P in _dataContext.ref_Pays
                    select P.LibellePays).ToList();

        }

        public IPays GetPays(int IdPays)
        {
            return (from P in _dataContext.ref_Pays
                    where P.IdPays == IdPays
                    select P).SingleOrDefault();
        }


        public bool PaysExist(int IdPays)
        {
            return (GetPays(IdPays) != null);
        }
    

        public IPays GetPaysFrNoAccent2(string libelleFR)
        {

            libelleFR = libelleFR.Trim().ToLower().RemoveAccents();
            return (from P in _dataContext.ref_Pays
                    let libellePaysNoAccent = P.LibellePays.ToLower().RemoveAccents()
                    where libellePaysNoAccent == libelleFR
                    select P).SingleOrDefault();
        }

        public IPays GetPaysFr(string libelleFR)
        {
            libelleFR = libelleFR.Trim().ToLower();
            return (from P in _dataContext.ref_Pays
                    where P.LibellePays.ToLower() == libelleFR
                    select P).SingleOrDefault();
        }
        public IPays GetPaysENG(string libelleENG)
        {
            libelleENG = libelleENG.Trim().ToLower();
            return (from P in _dataContext.ref_Pays
                    where P.LibelleEngPays.ToLower() == libelleENG
                    select P).SingleOrDefault();
        }
    }
}
