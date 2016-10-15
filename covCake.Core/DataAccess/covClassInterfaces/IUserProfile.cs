using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public interface IUserProfile : ICovEntity
    {
         System.Guid UserId{get; set; }

         /// <summary>
         /// Attention le Setter modifie aussi dans le membership
         /// </summary>
         string UserName { get; set; }

         string Nom{get; set;}

         string Prenom{get; set;}

         bool Sexe{get; set;}

         string Ville{get; set;}

         string NumDepartement{get; set;}

         System.DateTime DateNaissance{get; set;}

         bool EmailPublique { get; set; }

         string Description{get; set;}

         string ImagePersoPath{get; set; }

         Guid? ActivationKey { get; set; }

      

         #region Non mapped Properies

         string PrenomNom { get; }
         string NomPrenom { get; }
         string Email { get; set; }
         string SexeLibelle { get; }
         string DisplayName { get; }
         DateTime AccountCreationDate { get; }
         DateTime LastActivityDate { get; }
         DateTime LastLoginDate { get; }
         DateTime LastPasswordChangedDate { get; }

         #endregion

         #region Relationship Objects

         IDepartement Departement { get; set; }

         IQueryable<IAlerte> Alertes { get; }

         IQueryable<IAbonnementProjet> AbonnementProjets { get;  }

         IQueryable<IMessagePrive> MessagesEnvoyes { get; }

         IQueryable<IMessagePrive> MessagesRecus {get;}

         IQueryable<IProjet> Projets {get; }

         #endregion
         //EntityRef<aspnetUser> aspnetUser { get; private set; }
    }
}
