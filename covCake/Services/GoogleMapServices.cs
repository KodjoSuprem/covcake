using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace covCake.Services
{
    public class GoogleMapServices
    {
        private const string _googleKey = "ABQIAAAANE808zIvEVkCKOlZZpqjlxQKmKCZGjN4B49v6XmOzHXUQ6SP-BSwmc7UDNHsmeWORa6g_ONFTZIYjg";

        private const string _googleGeoCodeUri = "http://maps.google.com/maps/geo?q=";

        private static string _outputType = "xml"; // Available options: csv, xml, kml, json

        private static string _ltd = "";

        public static string ISOtoTLD(string isoCode)
        {
            Dictionary<string,string> conv = new Dictionary<string,string>();
            conv["GB"] = "uk";
             
            if(!conv.ContainsKey(isoCode.ToUpper()))
            {
                return isoCode.ToLower();
            }
            return conv[isoCode.ToUpper()];
        }



      private static Uri GetGeocodeUri(string address)
      {
          //http://maps.google.com/maps/geo?q=1600+Amphitheatre+Parkway,+Mountain+View,+CA&output=json&oe=utf8&sensor=true_or_false&key=your_api_key
         address = HttpUtility.UrlEncode(address);
         return new Uri(String.Format("{0}{1}&output={2}&key={3}&gl={4}&sensor=false&oe=utf8", _googleGeoCodeUri, address, _outputType, _googleKey,_ltd));
      }


      private static string GetResponse(string address)
      {
         // WebClient client = new WebClient();
        
          Uri uri = GetGeocodeUri(address);
         System.Net.WebRequest req = HttpWebRequest.Create(uri);
            string resp = new StreamReader(req.GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
          //string resp = client.DownloadString(uri);

          return resp;
      }


      public static string GetCSV(string address)
      {
          return GetCSV(address, "");
      }

      public static string GetCSV(string address,string CodePays)
      {
          _ltd = (CodePays != "") ? ISOtoTLD(CodePays) : "";
          _outputType = "csv";
          return GetResponse(address);
      }

      public static string GetXML(string address)
      {
          return GetXML(address, "");
      }

      public static string GetXML(string address, string CodePays)
      {
          _ltd = (CodePays != "") ? ISOtoTLD(CodePays) : "";
          _outputType = "xml";
          return GetResponse(address);
      }

      public static string GetJson(string address)
      {
          return GetJson(address, "");
      }

      public static string GetJson(string address, string CodePays)
      {
          _ltd = (CodePays != "") ? ISOtoTLD(CodePays) : "";
          _outputType = "json";
          return GetResponse(address);


      }



    }
}
