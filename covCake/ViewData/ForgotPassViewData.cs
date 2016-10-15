using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.Models
{
    public class ForgotPassViewData
    {
        public string Email { get; set; }
        public bool Succeed { get; set; }
        public bool Error { get; set; }
        public string ErrorMsg { get; set; }
    }
}
