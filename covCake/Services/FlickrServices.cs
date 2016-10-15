using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Net;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace covCake.Services
{
    [XmlRoot("rsp")]
    public class FlickrPhotoList
    {
        
        [XmlElement("photos")]
        public FlickrPhotos Photos;

        public FlickrPhoto this[int index]
        {
            get
            {
                return Photos.PhotoList[index];
            }
        }

        public FlickrPhoto GetRandomPhoto()
        {
            return Photos.GetRandomPhoto();
        }
    }

   public class FlickrPhotos
    {
       private Random rand = new Random();
        
       [XmlElement("photo")]
       public FlickrPhoto[] PhotoList;

       [XmlAttribute("page")]
       public int CurrentPage;

       [XmlAttribute("perpage")]
       public int NbPhotosPerPage;

       [XmlAttribute("total")]
       public int TotalPhotos;

       [XmlAttribute("pages")]
       public int MaxPages;

           public FlickrPhoto GetRandomPhoto()
           {
               if (PhotoList == null || PhotoList.Length < 1)
                   return new FlickrPhoto();
               return PhotoList[rand.Next(PhotoList.Length )];
           }
    }


    public class FlickrPhoto
    {
        [XmlAttribute("url_m")]
        public string url_m;

        [XmlAttribute("url_s")]
        public string url_s;
        [XmlAttribute("url_t")]
        public string url_t;


        [XmlAttribute("title")]
        public string title;

      
    }

    public class FlickrServices
    {
        public const string ApiKey = "92e2946166e7507634884fe535d6f54e";
        public const string FlickrServicesUrl = "http://api.flickr.com/services/rest/";
        
        private static Random rand = new Random();
     
        public static Stream MakeRequest(NameValueCollection flickrOps,string methode)
        {
            flickrOps.Add("method", methode);
            flickrOps.Add("api_key", ApiKey);

            string queryString = flickrOps.ToQueryString();
            string reqUrl = FlickrServicesUrl + queryString;
            Uri flickServiceUri = new Uri(reqUrl);
            WebClient wc = new WebClient();

           // JavaScriptSerializer o;
           // System.Runtime.Serialization.Json.DataContractJsonSerializer l;

            return wc.OpenRead(flickServiceUri);
        }

        public static FlickrPhotoList SearchPhotos(string textSearch,int page)
        {
            string methode = "flickr.photos.search";
            NameValueCollection flickOps = new NameValueCollection();
            flickOps.Add("sort", "relevance");//date-posted-asc, date-posted-desc, date-taken-asc, date-taken-desc, interestingness-desc, interestingness-asc, and relevance.
            flickOps.Add("media", "photos");
            flickOps.Add("extras", "url_m,url_s,url_t");
            flickOps.Add("page", page.ToString());
            flickOps.Add("text", textSearch);

            //flickOps.Add("format", ""); xml par defaut
            //flickOps.Add("safe_search", "1");// safe par defaut si pas authentifier
            XmlSerializer xmlSerializer = null;
            Stream resp = null;
            FlickrPhotoList returnedPhotoList;
            try
            {
                resp = MakeRequest(flickOps, methode);
                xmlSerializer = new XmlSerializer(typeof(FlickrPhotoList));
                returnedPhotoList = xmlSerializer.Deserialize(resp) as FlickrPhotoList;
            }
            catch (Exception ex)
            {
                return new FlickrPhotoList();
            }

            //TODO: mettre en cache les images
            /*
            List<FlickrPhotoList> cachePhotos = CacheManager.Get(textSearch) as List<FlickrPhotoList>;
            if(cachePhotos== null)
                cachePhotos = new List<FlickrPhotoList>();
            if(cachePhotos.Count == 5)
                return cachePhotos[rand.Next(0,5)];
            else
            {
                cachePhotos.Add(returnedPhotoList);
                CacheManager.Insert(textSearch,cachePhotos,DateTime.Now.AddDays(1));
            }
            */

            return returnedPhotoList;
        }


        /// <summary>
        /// Fait une recherche de photo aléatoire sur flickr
        /// Effectue 2 requetes succesives
        /// </summary>
        /// <returns></returns>
        public static FlickrPhotoList SearchRandomPhotos(string textSearch)
        {
            FlickrPhotoList imgList = new FlickrPhotoList();
            int maxPage = imgList.Photos.MaxPages;
            return SearchPhotos(textSearch,rand.Next(1, maxPage ));
        }

        public static FlickrPhotoList SearchPhotos(string textSearch)
        {
            return SearchPhotos(textSearch, 1);
        }


        /// <summary>
        /// Prévoir des photos non tirée de flicker à rajouter
        /// au cas ou panne ou images de merde
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        public static FlickrPhotos GetDefaultPhotos(string textSearch)
        {
           // throw new NotImplementedException();
            return new FlickrPhotos();
        }

      
    }
}
