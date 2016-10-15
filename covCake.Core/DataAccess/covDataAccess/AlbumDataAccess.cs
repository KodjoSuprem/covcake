using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public class AlbumDataAccess : IAlbumDataAccess
    {
        private covCakeDataContext _dataContext;

        public AlbumDataAccess(covCakeDataContext datacontext)
        {
            _dataContext = datacontext;
        }
        public AlbumDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }
    }
}
