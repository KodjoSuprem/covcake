using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Diagnostics;

namespace covCake.DataAccess
{

    [DebuggerStepThrough]
    public partial class cov_UserProfile : IUserProfile
    {

        #region IUserProfile Members

        Guid IUserProfile.UserId
        {
            get
            {      
                return this.UserId;
            }
            set
            {
                this.UserId = value;
            }
        }


        /// <summary>
        /// Attention en utilisant le SETTER
        /// </summary>
        string IUserProfile.UserName
        {
            get
            {
                return this.aspnet_User.UserName;
            }
            set
            {
                this.aspnet_User.UserName = value;
                this.aspnet_User.LoweredUserName = value.ToLower();

            }
        }


        string IUserProfile.Nom
        {
            get
            {
                return this.Nom;
            }
            set
            {
                this.Nom = value;
            }
        }

        string IUserProfile.Prenom
        {
            get
            {
                return this.Prenom;
            }
            set
            {
                this.Prenom = value;
            }
        }

        bool IUserProfile.Sexe
        {
            get
            {
                return this.Sexe;
            }
            set
            {
                this.Sexe = value;
            }
        }

        string IUserProfile.Ville
        {
            get
            {
                return this.Ville;
            }
            set
            {
                this.Ville = value;
            }
        }



        DateTime IUserProfile.DateNaissance
        {
            get
            {
                return this.DateNaiss;
            }
            set
            {
                this.DateNaiss = value;
            }
        }

        bool IUserProfile.EmailPublique
        {
            get
            {
                return this.PublicMail;
            }
            set
            {
                this.PublicMail = value;
            }
        }

        string IUserProfile.Description
        {
            get
            {
                return this.SelfDesc;
            }
            set
            {
                this.SelfDesc = value;
            }
        }

        string IUserProfile.ImagePersoPath
        {
            get
            {

                return this.ImagePersoPath;

            }
            set
            {
                this.ImagePersoPath = value;
            }
        }

        IDepartement IUserProfile.Departement
        {
            get
            {
                return this.ref_Departement;
            }
            set
            {
                this.ref_Departement = (ref_Departement)value;
            }
           
        }
        string IUserProfile.NumDepartement
        {
            get
            {
                return this.NumDept;
            }
            set
            {
                this.NumDept = value;
            }
        }

        Guid? IUserProfile.ActivationKey
        {
            get
            {
                return this.ActivationKey;
            }
            set
            {
                this.ActivationKey = value;
            }
        }



        #endregion

        #region Non mapped Properies
        string IUserProfile.Email
        {
            get
            {
                return this.aspnet_User.aspnet_Membership.Email;
            }
            set
            {
                this.aspnet_User.aspnet_Membership.Email = value;
                this.aspnet_User.aspnet_Membership.LoweredEmail = value.ToLower();
            }
        }
        string IUserProfile.PrenomNom 
        {
            get
            {
                return this.Prenom.ToTitleCase() + " " + this.Nom.ToTitleCase();
            }
        }
        string IUserProfile.NomPrenom 
        {
            get
            {
                return this.Nom.ToTitleCase() + " " + this.Prenom.ToTitleCase();
            }
        }
        
        string IUserProfile.DisplayName
        {
            get
            {
                return this.Prenom.ToTitleCase() + " " + this.Nom[0].ToString().ToUpper() + ".";
            }
        }

       
        string IUserProfile.SexeLibelle
        {
            get
            {
                if (this.Sexe) return "Homme"; else return "Femme";
            }

        }


        DateTime IUserProfile.AccountCreationDate
        {
            get { return this.aspnet_User.aspnet_Membership.CreateDate; }
        }

        DateTime IUserProfile.LastActivityDate
        {
            get { return this.aspnet_User.LastActivityDate; }
        }

        DateTime IUserProfile.LastLoginDate
        {
            get { return this.aspnet_User.aspnet_Membership.LastLoginDate; }
        }

        DateTime IUserProfile.LastPasswordChangedDate
        {
            get { return this.aspnet_User.aspnet_Membership.LastPasswordChangedDate; }
        }
        #endregion
   
        #region Relationship Objects

        IQueryable<IAlerte> IUserProfile.Alertes
        {
            get
            {
                return this.cov_Alertes.Cast<IAlerte>().AsQueryable();
            }
        }

        IQueryable<IAbonnementProjet> IUserProfile.AbonnementProjets
        {
            get
            {
                return this.cov_AbonnementProjets.Cast<IAbonnementProjet>().AsQueryable();
            }

        }
        /// <summary>
        /// FromMessagePrives >> this.cov_MessagePrives
        /// </summary>
        IQueryable<IMessagePrive> IUserProfile.MessagesEnvoyes
        {
            get
            {
                return this.cov_MessagePrives.Cast<IMessagePrive>().AsQueryable(); ;
            }
        }

        IQueryable<IMessagePrive> IUserProfile.MessagesRecus
        {
            get
            {
                return this.cov_MessagePrives1.Cast<IMessagePrive>().AsQueryable(); ;
            }
        }

        IQueryable<IProjet> IUserProfile.Projets
        {
            get
            {
                return this.cov_Projets.Cast<IProjet>().AsQueryable(); ;
            }

        }

        #endregion


    }

    public static class UserProfileExtensions
    {
        /// <summary>
        /// Obtient le nombre de nouveau messages reçus
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int GetNewMessageCount(this IUserProfile user)
        {
              CovCakeData data = CovCake.DataProvider;
              int count = user.MessagesRecus.GetAllReceiverUnreadedMessages().Count();
              if(count > 1) count--;
              return count;

        }


        /// <summary>
        ///  Gets the information from the data source for the membership user associated
        ///  with this instance of UserProfile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static MembershipUser GetMembershipUser(this IUserProfile user)
        {
            return Membership.GetUser(user.UserId, false);
        }

        /// <summary>
        /// Obtient tout les projets ou user et mateuser participent
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mateUserId"></param>
        /// <returns></returns>
        public static IQueryable<IProjet> GetSharedProjects(this IUserProfile user, Guid mateUserId)
        {
            CovCakeData data = CovCake.DataProvider;
            return data.AbonnementProjetDataAccess.GetAllSharedProjects(user.UserId, mateUserId);
        }

        /// <summary>
        /// Indique si User participe au projet projId
        /// </summary>
        /// <param name="user"></param>
        /// <param name="projId"></param>
        /// <returns></returns>
        public static bool ParticipeA(this IUserProfile user, int projId)
        {
            CovCakeData data = CovCake.DataProvider;// new CovCakeData();
           
            IAbonnementProjet abo = data.AbonnementProjetDataAccess.GetAbonnement(user.UserId, projId); //data.AbonnementProjetDataAccess.GetAllAbonnementsOf(user.UserId).Where(a => a.ProjetId == proj.IdProjet);

            return (abo != null);
              
        }

        /// <summary>
        /// Indique si User participe au projet projId
        /// </summary>
        /// <param name="user"></param>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static bool ParticipeA(this IUserProfile user, IProjet proj)
        {
            return ParticipeA(user, proj.IdProjet);
        }

        public static string UserPhotoUrl(this IUserProfile user)
        {

            if(System.IO.File.Exists(HttpContext.Current.Server.MapPath(user.ImagePersoPath)))
            {
                return user.ImagePersoPath.Replace("~", "");
            }

            return CovCakeConfiguration.DefaultUserPhoto;

        }


    }

   


}
