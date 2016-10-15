using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public interface IDepartement : ICovEntity
    {
        string NumDept{get; set;}

        string NumRegion{get; set;}

        string NomDept{get; set;}

        IQueryable<IUserProfile> UserProfiles{get; }
    }
}
