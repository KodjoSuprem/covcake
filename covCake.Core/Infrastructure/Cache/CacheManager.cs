using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace covCake
{
    public class CacheManager
    {
        public static void Insert(string cacheKey, object value, DateTime absoluteDateExpiration)
        {
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteDateExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void Insert(string cacheKey, object value, TimeSpan timeOutExpiration)
        {

            HttpRuntime.Cache.Insert(cacheKey, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, timeOutExpiration);

        }

        /// <summary>
        ///  Retrieves the specified item from the System.Web.Caching.Cache object.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns>The retrieved cache item, or null if the key is not found.</returns>
        public static object Get(string cacheKey)
        {
           return HttpRuntime.Cache.Get(cacheKey);
        }

       
    }
}
