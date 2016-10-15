using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace covCake.Handlers
{
    public class AssetHandler : IHttpHandler
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {

            HttpContextWrapper baseCtx = new HttpContextWrapper(context);

            // Set the content type
           // response.ContentType = setting.ContentType;
            HttpResponseBase response = baseCtx.Response;
            // Compress
            baseCtx.CompressResponse();
           
            // Write
            Uri requestUrl = baseCtx.Request.Url;
            string assetFilePath = requestUrl.AbsolutePath;
            string serverSideFilePath = baseCtx.Server.MapPath(assetFilePath);
            string contentType;// = "text/plain";
            switch (Path.GetExtension(serverSideFilePath))
            {
                case ".js": contentType = "application/x-javascript";break;
                case ".css": contentType = "text/css"; break;
                default:
                    contentType = "text/plain"; break;
            }
            response.ContentType = contentType;
            response.WriteFile(serverSideFilePath);

            //File.ReadAllText(
            //using (StreamWriter sw = new StreamWriter(response.OutputStream))
            //{
            //    sw.Write(asset.Content);
            //}

        }

        #endregion
    }
}
