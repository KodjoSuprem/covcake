using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake;
using System.Diagnostics;

namespace covCake.DataAccess
{
    [DebuggerStepThrough]
    public class UserProfileDataAccess : covCake.DataAccess.IUserDataAccess 
    {
        private covCakeDataContext _dataContext;
        private ProjetDataAccess _projetDataAccess;
        private MessageDataAccess _messageDataAccess;

        public UserProfileDataAccess(covCakeDataContext dataBase, ProjetDataAccess projetDataAccess, MessageDataAccess messageDataAccess)
        {
            _dataContext = dataBase;
            _projetDataAccess = projetDataAccess;
            _messageDataAccess = messageDataAccess;
        }

        public UserProfileDataAccess(ProjetDataAccess projetDataAccess, MessageDataAccess messageDataAccess)
        {
            _dataContext = new covCakeDataContext();
            _projetDataAccess = projetDataAccess;
            _messageDataAccess = messageDataAccess;
        }


        #region Create
        public IUserProfile CreateUser()
        {
            return new cov_UserProfile();
        }
        public IUserProfile CreateUser(Guid id, string nom, string prenom, DateTime dateNaiss, bool sexe)
        {
            return new cov_UserProfile() { UserId = id, Nom = nom, Prenom = prenom, DateNaiss = dateNaiss, Sexe = sexe };
        }
        #endregion

        #region Insert
        public void InsertUser(IUserProfile user)
        {
            this._dataContext.cov_UserProfiles.InsertOnSubmit((cov_UserProfile)user);
            this._dataContext.SubmitChanges();
        }
        #endregion

        #region Update
        public void Save()
        {

            this._dataContext.SubmitChanges();
        }
        #endregion

        #region IUserDataAccess Members

        public IQueryable<IUserProfile> GetAllUsers()
        {
            return (from U in _dataContext.cov_UserProfiles
                    select U).Cast<IUserProfile>();
        }
        public IUserProfile GetUser(Guid userId)
        {
            return (from U in _dataContext.cov_UserProfiles
                    where U.UserId == userId
                    select U).SingleOrDefault();
        }
        public IUserProfile GetUser(string username)
        {
            return (from U in _dataContext.cov_UserProfiles
                    where U.aspnet_User.LoweredUserName.ToLower() == username.ToLower()
                    select U).SingleOrDefault();
        }
        public IUserProfile GetUserByMail(string email)
        {
            email = email.ToLower();
            return (from U in _dataContext.cov_UserProfiles
                    where U.aspnet_User.aspnet_Membership.LoweredEmail.ToLower() == email
                    select U).SingleOrDefault();
        }
        public IUserProfile GetUserByActKey(Guid actKey)
        {
            return (from U in _dataContext.cov_UserProfiles
                    where U.ActivationKey == actKey
                    select U).SingleOrDefault();
        }
        #endregion

   
    }


    public static class UserProfileDataAccessFilters
    {

        public static IQueryable<IUserProfile> UserRegisteredSince(this IQueryable<IUserProfile> query, DateTime date)
        {
            return (from U in query
                    where U.AccountCreationDate >= date
                    select U);
        }

    }

}
