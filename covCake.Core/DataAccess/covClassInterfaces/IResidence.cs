using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public interface IResidence : ICovEntity
    {
         int Id{get;set;}

         string Type{ get; set; }

         string Adresse { get; set; }

         double Prix { get; set; }

         int MaxHotes { get; set; }
    }
}
