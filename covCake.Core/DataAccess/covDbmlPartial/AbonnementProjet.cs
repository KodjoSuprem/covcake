using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public partial class cov_AbonnementProjet : IAbonnementProjet
    {

        #region IAbonnementProjet Members

        Guid IAbonnementProjet.UserId
        {
            get
            {
                return this.UserId;
            }
            set
            {
                this.UserId = value;
            }
        }

        int IAbonnementProjet.ProjetId
        {
            get
            {
                return this.IdProjet;
            }
            set
            {
                 this.IdProjet = value;
            }
        }

        IUserProfile IAbonnementProjet.UserProfile
        {
            get { return this.cov_UserProfile as IUserProfile; }
        }

        IProjet IAbonnementProjet.Projet
        {
            get { return this.cov_Projet as IProjet; }
        }


        DateTime IAbonnementProjet.DateAbonnement
        {
            get { return this.DateAbonnement; }
            set { this.DateAbonnement = value; }
        }

        #endregion
    }
}
