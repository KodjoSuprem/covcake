using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public interface IPaysDataAccess
    {

         IQueryable<IPays> GetAllPays();
         IList<string> GetAllPaysString();
         IPays GetPays(int idPays);
         IPays GetPaysFr(string libelle);
         IPays GetPaysENG(string libelle);
    }
}
