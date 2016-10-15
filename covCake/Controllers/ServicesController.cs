using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using covCake.Services;
using System.Web.Script.Serialization;
using System.Collections;
using covCake.DataAccess;

namespace covCake.Controllers
{
    public class ServicesController : BaseController
    {
        //
        // GET: /Services/


        public ContentResult ListePays(string q)
        {
            string list = "";

            foreach(string item in PaysServices.ListePaysString().Where(p => p.ToLower().Contains(q.ToLower())))
	        {
                list += item + "\n";
	        } 

            return Content(list);
        }

        public ContentResult Geocode(string q, string codePays)
        {
            string list = "";

            try
            {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IPays pays = Data.PaysDataAccess.GetPays(int.Parse(codePays));
           
           
                string rez = GoogleMapServices.GetJson(q, pays.CodePays2);

                Dictionary<string, object> rezMap = serializer.Deserialize<Dictionary<string, object>>(rez);
                if(((IDictionary<string, object>)rezMap["Status"])["code"] == "200")
                {
                    ArrayList placemarks = (ArrayList)rezMap["Placemark"];


                    foreach(var place in placemarks) //2 placemarks
                    {
                        list += ((IDictionary<string, object>)place)["address"] + "\n";// .Where(key;

                    }
                }
            }
            catch(Exception ex)
            {
            }
            return Content(list);//"text/json"
        }


        public ContentResult Geocode2(string q)
        {
            return Content(GoogleMapServices.GetJson(q));//"text/json"
        }

    }
}
