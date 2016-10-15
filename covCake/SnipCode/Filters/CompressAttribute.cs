using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
using System.Diagnostics;

namespace covCake
{
    [DebuggerStepThrough]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CompressAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        { 
            //HttpRequestBase request = filterContext.HttpContext.Request;
            //string acceptEncoding = request.Headers["Accept-Encoding"];
            //if(string.IsNullOrEmpty(acceptEncoding)) return;
            //acceptEncoding = acceptEncoding.ToUpperInvariant();


            //HttpResponseBase response = filterContext.HttpContext.Response;
            //if(acceptEncoding.Contains("GZIP"))
            //{
            //    response.AppendHeader("Content-Encoding", "gzip");
            //    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            //}
            //else if(acceptEncoding.Contains("DEFLATE"))
            //{
            //    response.AppendHeader("Content-Encoding", "deflate");
            //    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            //}
            if ((filterContext.Exception == null) || ((filterContext.Exception != null) && filterContext.ExceptionHandled))
            {
                filterContext.HttpContext.CompressResponse();
            }
            base.OnResultExecuted(filterContext);
        }
    }

}
