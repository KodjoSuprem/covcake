using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;
namespace covCake.Models
{
    public class ShowMessageViewData
    {
        public PagedList<IMessagePrive> MsgConnexes { get; set; }
        public IQueryable<IMessagePrive> MsgThread { get; set; }
        public IMessagePrive Msg { get; set; }
    }
}
