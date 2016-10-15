using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace covCake.DataAccess
{
    public interface IProjet : ICovEntity
    {
        string Commentaires { get; set; }
        DateTime DateCreation { get; set; }
        DateTime? DateDebut { get; set; }
        DateTime? DateFin { get; set; }

        int IdProjet { get; set; }
        Guid OwnerUserId { get; set; }
        int IdPaysDepart { get; set; }
 
        int IdPaysArrive { get; set; }

        bool Realise { get; set; }
        IResidence Residence { get; set; }
        string VilleArrive { get; set; }
        string VilleDepart { get; set; }
        int Visites { get; set; }

        bool Incertain { get; set; }

        DateTime DateModification { get; set; }

        int NbJours { get; set; }

        IQueryable<ITransport> Transports { get; }
        ICollection<cov_Transport> TransportsEntitySet { get; }
        //int IdTransport { get; set; }
        IUserProfile OwnerUserProfile { get; }
        IPays PaysArrive { get; set; }
        IPays PaysDepart { get; set; }
        ref_Pays PaysArriveEntity { get; set; }
        IQueryable<IMessagePrive> MessagePrives { get; }
        ICollection<cov_MessagePrive> MessagesEntitySet { get; }

        IQueryable<IAbonnementProjet> UserAbonnes { get; }
        ICollection<cov_AbonnementProjet> UserAbonnesEntitySet { get; }

        Duration GetDuree();

       // string DisplayName();
    }
}
