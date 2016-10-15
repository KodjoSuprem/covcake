using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public partial class ref_Departement : IDepartement
    {

        #region IDepartement Members

        string IDepartement.NumDept
        {
            get
            {
                return this.NumDept;
            }
            set
            {
                this.NumDept = value;
            }
        }

        string IDepartement.NumRegion
        {
            get
            {
                return this.NumRegion;
            }
            set
            {
                this.NumRegion = value;
            }
        }

        string IDepartement.NomDept
        {
            get
            {
                return this.NomDept;
            }
            set
            {
                this.NomDept = value;
            }
        }

        IQueryable<IUserProfile> IDepartement.UserProfiles
        {
            get
            {
                return this.cov_UserProfiles.Cast<IUserProfile>().AsQueryable();
            }
           
        }

        #endregion
    }
}
