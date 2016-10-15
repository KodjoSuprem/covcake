using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace covCake
{
    public static class NameValueCollectionExtension
    {

        public static string ToQueryString(this NameValueCollection col)
        {
            string qs ="?";
            foreach (string key in col.AllKeys)
	        {
        		qs += key +"="+ col[key].HtmlEncode() + "&";
	        }
            return qs.Remove(qs.Length-1,1);
        }
    }
}
