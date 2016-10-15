using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.Models
{
    public class ErrorRedirectViewData
    {

        public string Title { get; set; }
        public string ErrorMsg { get; set; }
        public string RedirectUrl
        {
            get;
            set;
        }
        public int DelaySeconds
        {
            get;
            set;
        }
    }
}
