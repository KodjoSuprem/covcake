using System.Web;
using System;
using System.Diagnostics;
namespace covCake
{
    public abstract class BaseHttpModule :  IHttpModule, IDisposable
    {

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => OnBeginRequest(new HttpContextWrapper(((HttpApplication) sender).Context));
            context.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication) sender).Context));
            context.EndRequest += (sender, e) => OnEndRequest(new HttpContextWrapper(((HttpApplication) sender).Context));
        }

        public virtual void OnBeginRequest(HttpContextBase context)
        {
        }

        public virtual void OnError(HttpContextBase context)
        {
        }

        public virtual void OnEndRequest(HttpContextBase context)
        {
        }

      
        [DebuggerStepThrough]
        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }
    }
}