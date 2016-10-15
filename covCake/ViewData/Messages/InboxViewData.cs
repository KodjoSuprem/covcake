using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake.DataAccess;


namespace covCake.Models
{
    public class InboxViewData
    {
       
        public PagedList<IMessagePrive> Messages;
        public TypeBoiteMessage TypeBoite;

    }
}
