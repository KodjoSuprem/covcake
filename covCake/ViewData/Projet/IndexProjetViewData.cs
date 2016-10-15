using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;
using covCake.Services;

namespace covCake.Models
{
    public class IndexProjetViewData
    {
        public IProjet Projet { get; set; }
        public FlickrPhotos FlickrPictures { get; set; }

    }
}
