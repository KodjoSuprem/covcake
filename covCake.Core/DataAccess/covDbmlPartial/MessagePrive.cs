using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
     public enum TypeBoiteMessage
	 {
    	    Inbox  = 0,
            Outbox = 1,
            Trash  = 2,
            Saved  = 3  
     } 

    public partial class cov_MessagePrive : IMessagePrive
    {

        #region IMessagePrive Members

        int IMessagePrive.MsgId
        {
            get
            {
                return this.MsgId;
            }
           // set
            //{
              //  this.MsgId = value;
            //}
        }

        byte? IMessagePrive.TypeMessage
        {
            get
            {
                return this.TypeMessage;
            }
            set
            {
                this.TypeMessage = value;
            }
        }

        Guid IMessagePrive.FromUserId
        {
            get
            {
                return this.FromUserId;
            }
            set
            {
                this.FromUserId = value;
            }
        }

        Guid IMessagePrive.ToUserId
        {
            get
            {
                return this.ToUserId;
            }
            set
            {
                this.ToUserId = value;
            }
        }

        DateTime IMessagePrive.DateMessage
        {
            get
            {
                return this.DateMessage;
            }
            set
            {
                this.DateMessage = value;
            }
        }

        string IMessagePrive.Ip
        {
            get
            {
                return this.Ip;
            }
            set
            {
                this.Ip = value;
            }
        }

        bool IMessagePrive.IsFromNewMessage
        {
            get
            {
                return this.IsFromNewMessage;
            }
            set
            {
                this.IsFromNewMessage = value;
            }
        }

        bool IMessagePrive.IsToNewMessage
        {
            get
            {
                return this.IsToNewMessage;
            }
            set
            {
                this.IsToNewMessage = value;
            }
        }

        string IMessagePrive.SujetMessage
        {
            get
            {
                return this.Subject;
            }
            set
            {
                this.Subject = value;
            }
        }

        string IMessagePrive.TextMessage
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        int? IMessagePrive.ProjetRelatedId
        {
            get { return this.IdProjet; }
            set { this.IdProjet = value; }
        }

        bool IMessagePrive.InboxFromUserThreadDel 
        {
            get { return this.IFromUserDeleted; }
            set { this.IFromUserDeleted = value; }
        }

        bool IMessagePrive.OutboxFromUserThreadDel
        {
            get { return this.OFromUserDeleted; }
            set { this.OFromUserDeleted = value; }
        }

        bool IMessagePrive.InboxToUserThreadDel
        {
            get { return this.IToUserDeleted; }
            set { this.IToUserDeleted = value; }
        }

        bool IMessagePrive.OutboxToUserThreadDel
        {
            get { return this.OToUserDeleted; }
            set { this.OToUserDeleted = value; }
        }



        #region Relationship Object
        /// <summary>
        /// to >> 1
        /// </summary>
        IUserProfile IMessagePrive.ToUser
        {
            get
            {
                return this.cov_UserProfile1;
            }
        }

        IUserProfile IMessagePrive.FromUser
        {
            get
            {
                return this.cov_UserProfile;
            }

        }

        IProjet IMessagePrive.ProjetRelated
        {
            get { return this.cov_Projet as IProjet; }

        }

        int? IMessagePrive.MsgResponseId
        {
            get { return this.MsgResponseId; }
            set { this.MsgResponseId = value; }
        }

        /// <summary>
        /// cov_MessagePrive1 = Msg Pere
        /// </summary>
        IMessagePrive IMessagePrive.MsgResponse
        {
            get { return this.cov_MessagePrive1 as IMessagePrive; }
        }

        /// <summary>
        /// cov_MessagePrives = Msg qui répondent au message en cours
        /// </summary>
        IQueryable<IMessagePrive> IMessagePrive.MsgAnswers
        {
            get { return this.cov_MessagePrives.Cast<IMessagePrive>().AsQueryable(); }
        }


#endregion
        

        #endregion
    }

    public static class MessagePriveExtension
    {
        public static bool IsThreadHead(this IMessagePrive msg)
        {
            return (msg.MsgResponseId == null);
        }

        public static bool IsNewThread(this IMessagePrive msg)
        {

          //  throw new NotImplementedException();
            Guid currUserId = CovCake.GetCurrentUserId();
            if (msg.ToUserId == currUserId)
                return msg.IsToNewMessage;
            else if (msg.FromUserId == currUserId)
                return msg.IsFromNewMessage;
            else
                throw new Exception("Ce message ne devrait pas concerner cet utilisateur (ni ToUser ni FromUser). UserId: "+currUserId.ToString());
            //return (msg.MsgAnswers.NewMessagesFor(currUserId).Count() > 0);
          //  return (CovCake.DataProvider.MessageDataAccess.NewMessagesInThread(msg.MsgId, currUserId).Count() > 0);
            

        }

        public static IQueryable<IMessagePrive> NotDeleted(this IQueryable<IMessagePrive> query, TypeBoiteMessage boite)
        {
            //IQueryable<IMessagePrive> msgs;
            Guid currUserId = CovCake.GetCurrentUserId();
        
            switch (boite)
	        {
                case TypeBoiteMessage.Inbox:
                    return query.Where(m => (m.InboxToUserThreadDel == false && m.ToUserId == currUserId) || 
                                            (m.InboxFromUserThreadDel == false && m.FromUserId == currUserId));
                    break;
                case TypeBoiteMessage.Outbox:
                    return query.Where(m => (m.OutboxToUserThreadDel == false && m.ToUserId == currUserId) ||
                                             (m.OutboxFromUserThreadDel == false && m.FromUserId == currUserId));
                    break;
                default:
                    return query;
	        }
            
        }
    }

}
