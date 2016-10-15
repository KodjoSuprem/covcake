using System;
namespace covCake.DataAccess
{
    public interface IAlerte
    {
        DateTime? DateDebutProjet { get; set; }
        int IdAlerte { get;}// set; }
        int? IdPays { get; set; }
        int? NbJours { get; set; }
        DateTime? PeriodeDebut { get; set; }
        DateTime? PeriodeFin { get; set; }
        Guid UserId { get; set; }
        string VilleArrive { get; set; }

        DateTime DateCreation { get; set; }
        
        DateTime DateDernierEnvoi { get; set; }

        IUserProfile UserProfile { get; }
        IPays PaysArrive { get;}
    }
}
