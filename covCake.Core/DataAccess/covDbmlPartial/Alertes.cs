using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
    public partial class cov_Alerte : IAlerte
    {

        #region IAlerte Members

        IUserProfile IAlerte.UserProfile
        {
            get { return this.cov_UserProfile as IUserProfile; }
        }

        IPays IAlerte.PaysArrive
        {
            get { return this.ref_Pays as IPays; }
        }


        string IAlerte.VilleArrive
        {
            get { return this.VilleArrive;  }

            set { this.VilleArrive = value; }
        }

        DateTime IAlerte.DateDernierEnvoi
        {
            get
            {
                return this.DateDernierEnvoi;
            }

            set
            {
                this.DateDernierEnvoi = value;
            }
        }

        DateTime IAlerte.DateCreation
        {
            get
            {
                return this.DateCreation;
            }

            set
            {
                this.DateCreation = value;
            }
        }

        DateTime? IAlerte.DateDebutProjet
        {
            get
            {
                return this.DateDebutProjet;
            }
          
            set
            {
                this.DateDebutProjet = value;
            }
        }

        int IAlerte.IdAlerte
        {
            get
            {
                return this.IdAlerte;
            }
           /*set
            {
                this.IdAlerte = value;
            }*/
        }

        int? IAlerte.IdPays
        {
            get
            {
                return this.IdPays;
            }
            set
            {
                this.IdPays = value;
            }
        }

        int? IAlerte.NbJours
        {
            get
            {
                return this.NbJours;
            }
            set
            {
                this.NbJours = value;
            }
        }

        DateTime? IAlerte.PeriodeDebut
        {
            get
            {
                return this.PeriodeDebut;
            }
            set
            {
                this.PeriodeDebut = value;
            }
        }

        DateTime? IAlerte.PeriodeFin
        {
            get
            {
                return this.PeriodeFin;
            }
            set
            {
                this.PeriodeFin = value;
            }
        }

        Guid IAlerte.UserId
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

        #endregion
    }
}
