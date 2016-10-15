using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using covCake;
using System.Collections;

namespace covCake.DataAccess
{
    public partial class cov_Projet : IProjet
    {

        #region IProjet Members

        string IProjet.Commentaires
        {
            get
            {
                return Commentaire;
            }
            set
            {
                Commentaire = value;
            }
        }

        DateTime IProjet.DateCreation
        {
            get
            {
                return DateCreation;
            }
            set
            {
                DateCreation = value;
            }
        }

        DateTime? IProjet.DateDebut
        {
            get
            {
                return DateDebut;
            }
            set
            {
                DateDebut = value;
            }
        }

        DateTime? IProjet.DateFin
        {
            get
            {
                return DateFin;
            }
            set
            {
                DateFin = value;
            }
        }

  

        int IProjet.IdProjet
        {
            get
            {
                return IdProjet;
            }
            set
            {
                IdProjet = value;
            }
        }


        int IProjet.IdPaysArrive
        {
            get
            {
                return this.IdPaysArrive;
            }
            set
            {
                this.IdPaysArrive = value;
            }
        }


        int IProjet.IdPaysDepart
        {
            get
            {
                return this.IdPaysDepart;
            }
            set
            {
                this.IdPaysDepart = value;
            }
        }




        Guid IProjet.OwnerUserId
        {
            get
            {
                return this.OwnerUserId;
            }
            set
            {
                this.OwnerUserId = value;
            }
        }


        int IProjet.Visites
        {
            get { return this.Visites; }
            set { this.Visites = value; }
        }

        IPays IProjet.PaysArrive
        {
            get
            {
                return this.ref_Pays;
            }
            set
            {
                this.ref_Pays = (ref_Pays)value;
            }
        }

        ref_Pays IProjet.PaysArriveEntity
        {
            get
            {
                return this.ref_Pays;
            }
            set
            {
                this.ref_Pays = (ref_Pays)value;
            }
        }


        /// <summary>
        /// depart ref_Pays1
        /// </summary>
        IPays IProjet.PaysDepart
        {
            get
            {
                return this.ref_Pays1;
            }
            set
            {
                this.ref_Pays1 = (ref_Pays)value;
            }
        }

        bool IProjet.Realise
        {
            get
            {
                return Realise;
            }
            set
            {
                Realise = value;
            }
        }

      

        IResidence IProjet.Residence
        {
            get
            {
                return this.cov_Residence;
            }
            set
            {
                this.cov_Residence = (cov_Residence)value;
            }
        }

        IUserProfile IProjet.OwnerUserProfile
        {
            get
            {

                return this.cov_UserProfile;
            }
          
        }

        string IProjet.VilleArrive
        {
            get
            {
                return this.VilleArrive;
            }
            set
            {
                this.VilleArrive = value;
            }
        }

        string IProjet.VilleDepart
        {
            get
            {
                return this.VilleDepart;
            }
            set
            {
                this.VilleDepart = value;
            }
        }


        bool IProjet.Incertain { get {return this.Incertain;} set{ this.Incertain = value;} }

        DateTime IProjet.DateModification { get { return this.DateModification; } set { this.DateModification = value; } }

        int IProjet.NbJours { get { return this.NbJours; } set { this.NbJours = value; } }

        IQueryable<ITransport> IProjet.Transports
        {
            get { return this.cov_Transports.Cast<ITransport>().AsQueryable(); }
        }

        ICollection<cov_Transport> IProjet.TransportsEntitySet
        {
            get { return this.cov_Transports; }
            //set { this.}
        }

        IQueryable<IMessagePrive> IProjet.MessagePrives
        {
            get
            {
                return this.cov_MessagePrives.Cast<IMessagePrive>().AsQueryable();
            }
        }

        ICollection<cov_MessagePrive> IProjet.MessagesEntitySet
        {
            get {return this.cov_MessagePrives;}
        }

        IQueryable<IAbonnementProjet> IProjet.UserAbonnes
        {
            get { return this.cov_AbonnementProjets.Cast<IAbonnementProjet>().AsQueryable(); }
        }

      

        ICollection<cov_AbonnementProjet> IProjet.UserAbonnesEntitySet
        {
            get { return this.cov_AbonnementProjets; }
        }

        #endregion



        Duration IProjet.GetDuree()
        {
            if(Incertain)
            {
                return new Duration { Jours = this.NbJours, timeSpan = new TimeSpan(this.NbJours,0,0,0) };
            }
            else
            {
                if(DateDebut == null && DateFin == null)
                    throw new Exception("Le projet ne possede pas de date de début ou de fin alors qu'il n'est pas certain!");
                return DateDebut.Value.GetDuration(DateFin.Value);
            }
         
        }

      
       
    }

    public static class ProjetExtension
    {
        public struct GmapPoint
        {
            public string X { get; set; }
            public string Y { get; set; }
        }

        public static string GetGeolocAddress(this IProjet proj)
        {
            string adr = "";

            if(proj.Residence != null)
            {
                adr = proj.Residence.Adresse;// +" " + proj.PaysArrive.CodePays + " " + proj.PaysArrive.LibellePays;
            }
            adr += (" " + proj.PaysArrive.CodePays + " " + proj.PaysArrive.LibellePays);
            return adr;
        }

        [Obsolete("pas utiliser en attendant")]
        public static GmapPoint GetLatLong(this IProjet proj)
        {
           /*
            string adr = "";
            
            if(proj.Residence != null)
            {
                adr = proj.Residence.Adresse + " " + proj.PaysArrive.CodePays + " " + proj.PaysArrive.LibellePays;
            }


            string[] csv =  {"0","0","0","0"};
            try{
                csv = Services.GoogleMapServices.GetCSV(adr).Split(',');
            }
            catch(Exception ex)
            {

            }
            return new GmapPoint() { X = csv[2], Y = csv[3] };*/
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reoutrne DateDebut.ToShortDateString() ou la chaine passé en paramettre si null
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="replaceText"></param>
        /// <returns></returns>
        public static string GetShortDateStringOr(this IProjet proj, string replaceText)
        {
            return (proj.DateDebut.HasValue) ? proj.DateDebut.Value.ToShortDateString() : replaceText;
        }

        /// <summary>
        /// Renvoi DateDebut.ToShortDateString() ou la chaine "Incertain" si null
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static string GetShortDateStringOr(this IProjet proj)
        {
            return (proj.DateDebut.HasValue) ? proj.DateDebut.Value.ToShortDateString() : "Incertain";
        }

        /// <summary>
        /// Retourne DateDebut.ToShortDateString() ou GetDuree().ToString() si null
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static string GetShortDateStringOrDuration(this IProjet proj)
        {
            return (proj.DateDebut.HasValue) ? proj.DateDebut.Value.ToShortDateString() : proj.GetDuree().ToString();
        }

        /// <summary>
        /// Affiche {depart -> paysArrivé} le {date} pendant {nbjours} jours
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static string GetLongDisplayName(this IProjet proj)
        {
            string villedep = (!string.IsNullOrEmpty(proj.VilleDepart)) ? proj.VilleDepart + " -> " : "";
            string dest = villedep + proj.PaysArrive.LibellePays;
            if(!proj.Incertain)
                return dest + " le " + proj.DateDebut.Value.ToShortDateString() + " pendant " + proj.GetDuree();
            else
                return dest + " pendant " + proj.NbJours + " jours";
        }

        public static string GetDisplayName(this IProjet proj)
        {
            return proj.GetProjectTitle() + " pendant " + proj.GetDuree().ToString();
        }


        /// <summary>
        /// Si date aujourdhui strict sup à date début projet
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static bool IsOutDated(this IProjet proj)
        {
             if(!proj.DateDebut.HasValue) return false;
             return (DateTime.Now > proj.DateDebut);
        }

        /// <summary>
        /// retourne {paysArrive} ({villeArrive})
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static string GetProjectTitle(this IProjet proj)
        {
            string villeString = (!proj.VilleArrive.IsNullOrEmpty()) ? " (" + proj.VilleArrive + ")" : "";
            return proj.PaysArrive.LibellePays + villeString;
        }

        /// <summary>
        /// {pays} le {date} ou {pays} ({nbJours} jours)
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static string GetShortDisplayName(this IProjet proj)
        {
            if(!proj.Incertain)
                return  proj.PaysArrive.LibellePays + " le " + proj.DateDebut.Value.ToShortDateString();
            return proj.PaysArrive.LibellePays + " (" + proj.NbJours + " jours)";
        }

        /// <summary>
        /// Voyage vers {pays} le {date} avec {ownerDisplayname}
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static string GetFriendDisplayName(this IProjet proj)
        {
            int nbSubscribers = proj.UserAbonnes.Count();
            string friendsLabel = (nbSubscribers > 0) ? (++nbSubscribers) + " personnes" : proj.OwnerUserProfile.DisplayName;
            return "Voyage vers " + proj.GetShortDisplayName() + " avec " + friendsLabel;
        }


        /// <summary>
        /// Si projet pas incertain retourne durée avant départ
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static TimeSpan GetTimeFromDeparture(this IProjet proj)
        {
            if(!proj.Incertain)
                if(proj.DateDebut.HasValue)
                    return proj.DateDebut.Value - DateTime.Now;
            return TimeSpan.Zero;
        }
    }

}
