using System;
namespace covCake.DataAccess
{
   public interface IMessageDataAccess
    {
        void DeleteMessage(int MsgId);
        void DeleteMessages(System.Collections.Generic.IEnumerable<int> MsgIds);
        System.Linq.IQueryable<IMessagePrive> GetAllMessagesSince(DateTime date);
        System.Linq.IQueryable<IMessagePrive> GetAllMessages();
        System.Linq.IQueryable<IMessagePrive> GetAllMessagesFrom(string username);
        System.Linq.IQueryable<IMessagePrive> GetAllMessagesFrom(Guid UserId);
        System.Linq.IQueryable<IMessagePrive> GetAllMessagesTo(Guid UserId);
        System.Linq.IQueryable<IMessagePrive> GetAllMessagesTo(string username);
        System.Linq.IQueryable<IMessagePrive> GetAllUnreadedMessagesTo(string username);
        IMessagePrive GetMessage(int MsgId);
        //void SaveMessage(Guid fromUser, Guid toUser, string subject, string text, string fromIP, byte msgType);
        void SetMessageRead(int MsgId);
        void SetMessageUnRead(int MsgId);

        void Save();

        IMessagePrive CreateMessage();
        void InsertMessage(IMessagePrive msg);

        void SubmitChanges();
    }
}
