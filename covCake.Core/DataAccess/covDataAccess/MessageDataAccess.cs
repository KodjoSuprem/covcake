using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Diagnostics;
using System.Collections;

namespace covCake.DataAccess
{
      
   


    public class MessageDataAccess 
    {
        public class MessageThreadIdComparer : IEqualityComparer<IMessagePrive>
        {

            #region IEqualityComparer<IMessagePrive> Members

            public bool Equals(IMessagePrive x, IMessagePrive y)
            {
                return (x.MsgResponseId == y.MsgResponseId);
            }

            public int GetHashCode(IMessagePrive obj)
            {
                return obj.GetHashCode();
            }

            #endregion
        }

        private covCakeDataContext _dataContext;

        public MessageDataAccess(covCakeDataContext dataBase)
        {
            _dataContext = dataBase;
        }

        public MessageDataAccess()
        {
            _dataContext = new covCakeDataContext();
        }


        public void SubmitChanges()
        {
            this._dataContext.SubmitChanges();
        }

        #region Create
        public IMessagePrive CreateMessage()
        {
            return new cov_MessagePrive();
        }

        public IMessagePrive CreateMessage(Guid fromUserId, Guid toUserId,string subject, string text)
        {
            return new cov_MessagePrive() {FromUserId = fromUserId, ToUserId = toUserId, Subject = subject, Text = text };
        }
        #endregion

        #region Insert
        public void InsertMessage(IMessagePrive message)
        {
            this._dataContext.cov_MessagePrives.InsertOnSubmit((cov_MessagePrive)message);
            this._dataContext.SubmitChanges();
        }
        /*
        public void SaveMessage(Guid fromUserId, Guid toUserId, string sujetMsg, string text, string fromIP, byte msgType)
        {
            // Guid newMsgId = Guid.NewGuid();
            IMessagePrive newMsg = new cov_MessagePrive();
            newMsg.IsNewMessage = true;
            newMsg.Ip = fromIP;
            newMsg.FromUserId = fromUserId;
            newMsg.ToUserId = toUserId;
            newMsg.SujetMessage = sujetMsg;
            newMsg.TextMessage = text;
            newMsg.TypeMessage = msgType;

            _dataContext.cov_MessagePrives.InsertOnSubmit((cov_MessagePrive)newMsg);
            _dataContext.SubmitChanges();
            // return newMsgId;

        }*/
        #endregion

        #region Queries

        
        public IQueryable<IMessagePrive> GetAllMessages()
        {
            return (from M in _dataContext.cov_MessagePrives
                    select M).Cast<IMessagePrive>();
        }

        public IQueryable<IMessagePrive> GetAllMessagesFrom(string username)
        {
            return (from M in _dataContext.cov_MessagePrives
                   join U in _dataContext.cov_UserProfiles on
                   M.FromUserId equals U.UserId
                   select M).Cast<IMessagePrive>();
        }

        public  IQueryable<IMessagePrive> GetAllMessagesTo(string username)
        {

            return (from M in _dataContext.cov_MessagePrives
                    join U in _dataContext.cov_UserProfiles on
                    M.ToUserId equals U.UserId
                    select M).Cast<IMessagePrive>();
        }

        public IQueryable<IMessagePrive> GetAllMessagesFrom(Guid UserId)
        {
            return (from M in _dataContext.cov_MessagePrives
                    where M.FromUserId == UserId
                    select M).Cast<IMessagePrive>();
        }

        public  IQueryable<IMessagePrive> GetAllMessagesTo(Guid UserId)
        {

            return (from M in _dataContext.cov_MessagePrives
                    where M.ToUserId == UserId
                    select M).Cast<IMessagePrive>();
        }

        public IQueryable<IMessagePrive> GetAllMessagesSince(DateTime date)
        {
            return (from M in _dataContext.cov_MessagePrives
                    where M.DateMessage >= date
                    select M).Cast<IMessagePrive>();
        }

        public IQueryable<IMessagePrive> GetAllUnreadedMessagesTo(string username)
        {
            //sa gere ;) !! 
            return GetAllMessagesTo(username).Where(MessagePrive => MessagePrive.IsToNewMessage == true);
        }
        
        public IMessagePrive GetMessage(int MsgId)
        {
            return (from M in _dataContext.cov_MessagePrives
                    where M.MsgId == MsgId
                    select M).SingleOrDefault();
        }


        /// <summary>
        /// Récupere tout les messages du thread auquel appartient le message courant.
        /// OrderBy DateMessage
        /// </summary>
        /// <param name="MsgId"></param>
        /// <returns></returns>
        public IQueryable<IMessagePrive> GetAllMsgThread(int MsgId)
        {
            return (from M in _dataContext.cov_MessagePrives
                    let MsgResp = this.GetMessage(MsgId) //Msg Lu
                    where M.MsgId == MsgId
                    || M.MsgResponseId == MsgResp.MsgResponseId //Tout les fils du pere du Msg Lu
                    || M.MsgId == MsgResp.MsgResponseId  //Le message pere
                    || M.MsgResponseId == MsgId //tt les messages qui réponde a celui la (top thread)
                    orderby M.DateMessage
                    select M).Cast<IMessagePrive>();
        }

        /// <summary>
        /// Retourne toutes les têtes de thread
        /// (MsgResponseId == NULL)
        /// </summary>
        /// <returns></returns>
        public IQueryable<IMessagePrive> GetAllThreadHeads()
        {
            return (from M in this._dataContext.cov_MessagePrives
                    where M.MsgResponseId == null
                    select M).Cast<IMessagePrive>(); ;
        }

        #region Delete
        public void DeleteMessages(IEnumerable<IMessagePrive> msgs)
        {
            _dataContext.cov_MessagePrives.DeleteAllOnSubmit(msgs.Cast<cov_MessagePrive>());
            _dataContext.SubmitChanges();
        }

        public void DeleteMessage(int MsgId)
        {
            IMessagePrive msg = GetMessage(MsgId);
            _dataContext.cov_MessagePrives.DeleteOnSubmit((cov_MessagePrive)msg);
            _dataContext.SubmitChanges();

        }
        public void DeleteMessages(IEnumerable<int> MsgIds)
        {
            IMessagePrive msg;
            foreach (int id in MsgIds)
            {
                msg = GetMessage(id);
                _dataContext.cov_MessagePrives.DeleteOnSubmit((cov_MessagePrive)msg);
            }
            _dataContext.SubmitChanges();

        }
        #endregion
        
        #region Update

        public void SetMessageRead(int msgId)
        {
            IMessagePrive msg = GetMessage(msgId);
            msg.IsFromNewMessage = false;
            msg.IsToNewMessage = false;
            _dataContext.SubmitChanges();
        }


        public void SetMessageReadForSender(int msgId)
        {
            IMessagePrive msg = GetMessage(msgId);
            msg.IsFromNewMessage = false;
            _dataContext.SubmitChanges();
        }

        public void SetMessageUnReadForReceiver(int msgId)
        {
            IMessagePrive msg = GetMessage(msgId);
            msg.IsToNewMessage = true;
            _dataContext.SubmitChanges();
        }
        #endregion

        #region Save
        public void Save()
        {
            //_dataContext.SubmitChanges(ConflictMode.FailOnFirstConflict); 
            _dataContext.SubmitChanges();

        }
        #endregion
        
        #endregion

        public void SetDeleted(IEnumerable<IMessagePrive> msgsFiltered, Guid guid, TypeBoiteMessage folder)
        {
            //msgsFiltered.ForEach(m => { if(m.FromUserId == guid) m.FromUserThreadDeleted = true; else m.ToUserThreadDeleted = true; });
            foreach (IMessagePrive mes in msgsFiltered)
            {
                if (mes.FromUserId == guid)
                    if (folder ==  TypeBoiteMessage.Inbox)
                        mes.InboxFromUserThreadDel = true;
                    else
                        mes.OutboxFromUserThreadDel = true;
                else
                    if (folder == TypeBoiteMessage.Inbox)
                        mes.InboxToUserThreadDel = true;
                    else
                        mes.OutboxToUserThreadDel = true;
            }

            _dataContext.SubmitChanges();
        }


        public void SetAllMsgThreadDeleted(int firstMsgThread, Guid guid, TypeBoiteMessage boite)
        {
            IQueryable<IMessagePrive> msg = GetAllMsgThread(firstMsgThread);

            this.SetDeleted(msg, guid, boite);
        }


       

    }

    public static class MessageDataAccessExtensions
    {

        public static IQueryable<IMessagePrive> GetAllMessagesFrom(this IQueryable<IMessagePrive> query, Guid fromUserId)
        {
            return query.Where(m => m.FromUserId == fromUserId);
        }

        public static IQueryable<IMessagePrive> NewMessagesFor(this IQueryable<IMessagePrive> query, Guid userId)
        {
            return (from M in query
                    where M.ToUserId == userId && M.IsToNewMessage == true
                    select M);
        }

        /// <summary>
        /// Renvoi tt les tête de threads contenus dans la liste des messages
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<IMessagePrive> GetAllThreadHeads(this IQueryable<IMessagePrive> msgRecusQuery)
        {

            //query = tout les messages reçus...
            //Selection du dernier message répondant a celui ci
            //-> Liste msg réçus
            //-> retrouver les tête de thread 
            return     (from Mrec in msgRecusQuery
                        from Mall in CovCake.DataProvider.CovCakeDataContext.cov_MessagePrives
                        where (Mrec.MsgResponseId == null && Mrec.MsgId == Mall.MsgId )
                        || Mrec.MsgResponseId == Mall.MsgId
                        select Mall).Distinct().Cast<IMessagePrive>();
        }


        /// <summary>
        /// Pour chaque tête de thread renvoi le dernier message de la conversation
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<IMessagePrive> GetThreadLastMessage(this IQueryable<IMessagePrive> query)
        {
            return (from M in query
                    from Ans in M.MsgAnswers
                    where Ans.DateMessage == M.MsgAnswers.Max(MM => MM.DateMessage)
                    select Ans);
        }

        [Obsolete("Cheulou...je sais même plus ske sa fait...Ne pas utiliser")]
        public static IQueryable<IMessagePrive> GetAllThreadsX(this IQueryable<IMessagePrive> query)
        {
            throw new NotSupportedException("Obsolete, dangereux, ne pas utiliser....");
            //IQueryable<IMessagePrive> allThread = (from M in query
            //                                       where M.MsgResponse != null
            //                                       select M.MsgResponse).Distinct();

            //var fuck = allThread.Concat(query.Where(m => m.MsgResponseId == null));
            
            /*
             * 
             * 
             * 
                var fuck = (from M in query
                            group M by M.MsgResponseId into MsgbyThread
                            select new 
                            { 
                                MsgbyThread.Key, 
                                LatestMsg = 
                                    (from LM in MsgbyThread
                                    where LM.DateMessage == MsgbyThread.Max(MM => MM.DateMessage)
                                    select LM).LastOrDefault()
                            });
             * 
             * 
             * 
             */

            IQueryable<IMessagePrive> allLatestMsgFromThread = (from M in query
                        group M by M.MsgResponseId into MsgbyThread
                        select (from LM in MsgbyThread
                                where LM.DateMessage == MsgbyThread.Max(MM => MM.DateMessage)
                                select LM).LastOrDefault());
       

            return (from MM in allLatestMsgFromThread.Distinct()
                    where !(from M in allLatestMsgFromThread
                            from O in allLatestMsgFromThread
                            where M.MsgId == O.MsgResponseId     
                            select M.MsgId).Contains(MM.MsgId)
                    select MM);


        }

         public static IQueryable<IMessagePrive> GetAllThreadsNotDeleted(this IQueryable<IMessagePrive> query, TypeBoiteMessage boite)
         {
             return GetAllThreadHeads(query).NotDeleted(boite);
         }

        
        public static IQueryable<IMessagePrive> GetAllReceiverUnreadedMessages(this IQueryable<IMessagePrive> query)
        {
            return query.Where(msg => msg.IsToNewMessage == true);
        }

        public static IQueryable<IMessagePrive> GetAllSenderUnreadedMessages(this IQueryable<IMessagePrive> query)
        {
            return query.Where(msg => msg.IsFromNewMessage == true);
        }

        /// <summary>
        /// Retourne tout les messages qui concernent un projet
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
         public static IQueryable<IMessagePrive> GetAllProjetsRelatedMessages(this IQueryable<IMessagePrive> query)
        {
            return (from M in query
                    where M.ProjetRelatedId != null
                    select M);
        }


         public static IQueryable<IMessagePrive> GetAllMessageBefore(this IQueryable<IMessagePrive> query, DateTime date)
         {
             return GetAllMessagesBetween(query, new DateTime(1,1,1)  , date);
         }

        public static IQueryable<IMessagePrive> GetAllMessagesSince(this IQueryable<IMessagePrive> query, DateTime date)
        {
            return GetAllMessagesBetween(query, date, DateTime.Now);
        }

        public static IQueryable<IMessagePrive> GetAllMessagesBetween(this IQueryable<IMessagePrive> query, DateTime dateDebut, DateTime dateFin)
        {
            if(dateDebut > dateFin)
                throw new DateTimeException("La date de fin ne peut être anterieur à la date de debut");
            return (from M in query
                    where M.DateMessage >= dateDebut && M.DateMessage <= dateFin
                    select M);
        }
    }


}
