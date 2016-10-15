using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public interface IMessagePrive : ICovEntity
    {

         int MsgId{get;}

         byte? TypeMessage{get; set;}

         System.Guid FromUserId{get; set;}

         System.Guid ToUserId{get; set;}

         System.DateTime DateMessage{get; set;}

         string Ip{get; set;}

         bool IsFromNewMessage{get; set;}

         bool IsToNewMessage { get; set; }

         string SujetMessage{get; set;}

         string TextMessage{get; set; }

         int? ProjetRelatedId { get; set; }

         int? MsgResponseId { get; set; }


         bool InboxToUserThreadDel { get; set; }

         bool InboxFromUserThreadDel { get; set; }


         bool OutboxToUserThreadDel { get; set; }

         bool OutboxFromUserThreadDel { get; set; }

         #region #Relationship objects
         IUserProfile ToUser { get; }
         IUserProfile FromUser { get; }
         IProjet ProjetRelated { get; }
         IMessagePrive MsgResponse { get; }
         IQueryable<IMessagePrive> MsgAnswers { get; }
         #endregion 



    }
}
