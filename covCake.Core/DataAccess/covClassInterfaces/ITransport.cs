using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public interface ITransport : ICovEntity
    {
         int IdTransport{get;}

         int IdProjet{get;set;}

         string ModeTransport{get;set;}

         string Compagnie{get;set;}

         string NumVol{get;set;}

         double PrixAR{get;set;}

         string Details{get;set;}

         IProjet ProjetRelated{get;}
    }
}
