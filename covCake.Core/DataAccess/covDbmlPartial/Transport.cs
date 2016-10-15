using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public partial class cov_Transport : ITransport
    {

        #region ITransport Members
        public static string[] MODETRANSPORT = new string[] { "AVION", "TRAIN", "BATEAU", "VOITURE", "AUTRE" };


        int ITransport.IdTransport
        {
            get
            {
                return this.IdTransport;
            }
          
        }

        int ITransport.IdProjet
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

        string ITransport.ModeTransport
        {
            get
            {
                return this.Mode;
            }
            set
            {
                this.Mode = value;
            }
        }

        string ITransport.Compagnie
        {
            get
            {
                return this.Compagnie;
            }
            set
            {
                this.Compagnie = value;
            }
        }

        string ITransport.NumVol
        {
            get
            {
                return this.NumVol;
            }
            set
            {
                this.NumVol = value;
            }
        }

        double ITransport.PrixAR
        {
            get
            {
                return this.PrixAR;
            }
            set
            {
                this.PrixAR = value;
            }
        }

        string ITransport.Details
        {
            get
            {
                return this.Details;
            }
            set
            {
                this.Details = value;
            }
        }

        IProjet ITransport.ProjetRelated
        {
            get
            {
                return this.cov_Projet as IProjet;
            }
          
        }

        #endregion
    }
}
