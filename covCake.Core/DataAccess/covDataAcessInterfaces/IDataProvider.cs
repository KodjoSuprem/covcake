using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace covCake.DataAccess
{
   public interface IDataProvider
    {
       IUserDataAccess UserDataAccess { get; }
       IMessageDataAccess MessageDataAccess { get; }
       IProjetDataAccess ProjetDataAccess { get; }
       IAlbumDataAccess AlbumDataAccess { get; }
    }
}
