using System;
using System.Linq;
namespace covCake.DataAccess
{
    public interface IUserDataAccess 
    {
        IQueryable<IUserProfile> GetAllUsers();
        IUserProfile GetUser(Guid userId);
        IUserProfile GetUser(string username);
        IUserProfile GetUserByMail(string email);
        IUserProfile GetUserByActKey(Guid actKey);

        IUserProfile CreateUser();
        IUserProfile CreateUser(Guid id, string nom, string prenom, DateTime dateNaiss, bool sexe);
        void InsertUser(IUserProfile user);
        void Save();
    }
}
