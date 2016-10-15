using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public partial class cov_Residence : IResidence
    {
        #region IResidence Members

        public static string[] TYPESRESIDENCE = { "HOTEL", "CAMPING", "AUBERGE", "AMIS","CHEZ MOI", "AUTRE" };

        int IResidence.Id
        {
            get
            {
                return IdResidence;
            }
            set
            {
                IdResidence = value;
            }
        }

        string IResidence.Type
        {
            get
            {
                return TypeResidence;
            }
            set
            {
                TypeResidence = value;
            }
        }

        string IResidence.Adresse
        {
            get
            {
                return AdresseResidence;
            }
            set
            {
                AdresseResidence = value;
            }
        }

        #endregion

        #region IResidence Members


        double IResidence.Prix
        {
            get
            {
                return this.Prix;
            }
            set
            {
                this.Prix = value;
            }
        }


        int IResidence.MaxHotes
        {
            get { return this.MaxHotes ?? -1; }
            set { this.MaxHotes = value; }
        }
        #endregion
    }
}
