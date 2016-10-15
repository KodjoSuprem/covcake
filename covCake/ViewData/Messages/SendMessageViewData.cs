using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;

namespace covCake.Models
{
    public class SendMessageViewData // : System.Web.Mvc.DefaultModelBinder
    {
        public string relatedProjetId { get; set; }
        public string toUserId { get; set; }
        public string fromUserId { get; set; }
        public string message { get; set; }
        public string sujet { get; set; }
        public string responseMsgId { get; set; }

        public IUserProfile ToUserProfile { get; set; }
        public IProjet RelatedProjet { get; set; }
        public List<IUserProfile> Subscribers { get; set; }
       
       
    }
}
