using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO.Compression;
using System.Diagnostics;

namespace covCake
{
    [DebuggerStepThrough]
    public static class HttpContextExtension
    {
        public static void CacheResponseFor(this HttpContextBase context, TimeSpan duration)
        {
            HttpCachePolicyBase cache = context.Response.Cache;

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetLastModified(context.Timestamp);
            cache.SetExpires(context.Timestamp.Add(duration));
            cache.SetMaxAge(duration);
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }

        public static void CompressResponse(this HttpContextBase httpContext)
        {
            HttpRequestBase request = httpContext.Request;

            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding)) 
                return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = httpContext.Response;
            //HttpResponseBase response = filterContext.HttpContext.Response;
            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-Encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-Encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}
