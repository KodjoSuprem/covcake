using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace covCake.DataAccess
{

    /// <summary>
    /// Acces aux principaux dépot de données covCake
    /// </summary>
    public class CovCakeData : IDisposable
    {
        public UserProfileDataAccess UserDataAccess { get; private set; }
        public MessageDataAccess MessageDataAccess { get; private set; }
        public ProjetDataAccess ProjetDataAccess { get; private set; }
        public IAlbumDataAccess AlbumDataAccess { get; private set; }
        public PaysDataAccess PaysDataAccess { get; private set; }

        public AbonnementProjetDataAccess AbonnementProjetDataAccess { get; private set; }
        public DepartementDataAccess DepartementDataAcess { get; private set; }
        public ResidenceDataAccess ResidenceDataAccess { get; private set; }
        public TransportDataAccess TransportDataAccess { get; private set; }

        public AlerteDataAccess AlerteDataAccess { get; private set; }

       public covCakeDataContext CovCakeDataContext { get; private set; }

       public CovCakeData()
       {
            CovCakeDataContext = new covCakeDataContext();

            AbonnementProjetDataAccess = new AbonnementProjetDataAccess(CovCakeDataContext);
            MessageDataAccess = new MessageDataAccess(CovCakeDataContext);
            ProjetDataAccess = new ProjetDataAccess(CovCakeDataContext);
            UserDataAccess = new UserProfileDataAccess(CovCakeDataContext,ProjetDataAccess, MessageDataAccess);
            AlbumDataAccess = new AlbumDataAccess(CovCakeDataContext);
            PaysDataAccess = new PaysDataAccess(CovCakeDataContext);
            TransportDataAccess = new TransportDataAccess(CovCakeDataContext);
            ResidenceDataAccess = new ResidenceDataAccess(CovCakeDataContext);
            DepartementDataAcess = new DepartementDataAccess(CovCakeDataContext);
            AlerteDataAccess = new AlerteDataAccess(CovCakeDataContext);

       }

       #region IDisposable Members

       public void Dispose()
       {
           CovCakeDataContext.Dispose();
       }

       #endregion

       public void SubmitChanges()
       {
           this.CovCakeDataContext.SubmitChanges();
       }
    }
}

