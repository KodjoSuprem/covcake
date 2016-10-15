using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace covCake.DataAccess
{
    public interface IAbonnementProjet : ICovEntity
    {
        Guid UserId { get; set; }
        int ProjetId { get; set; }
        IUserProfile UserProfile { get; }
        IProjet Projet { get; }

        DateTime DateAbonnement { get; set; }
    }
}
