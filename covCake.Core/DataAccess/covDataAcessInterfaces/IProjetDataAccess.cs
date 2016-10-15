using System;
namespace covCake.DataAccess
{
    public interface IProjetDataAccess
    {

        System.Linq.IQueryable<IProjet> GetAllProjetBetween(DateTime debut, DateTime fin);
        System.Linq.IQueryable<IProjet> GetAllProjetFrom(int PaysId);
        System.Linq.IQueryable<IProjet> GetAllProjetFrom(string PaysLibelle);
        System.Linq.IQueryable<IProjet> GetAllProjetOf(Guid UserID);
        System.Linq.IQueryable<IProjet> GetAllProjetsOutdated();
        System.Linq.IQueryable<IProjet> GetAllProjetsRealise();
        System.Linq.IQueryable<IProjet> GetAllProjetTo(int PaysId);
        System.Linq.IQueryable<IProjet> GetAllProjetTo(string PaysLibelle);

        System.Linq.IQueryable<IProjet> GetAllProjets();
        IProjet GetProjet(int ProjetId);
        System.Linq.IQueryable<IProjet> GetAllProjetsNonRealise();

        IProjet CreateProjet();
        void InsertProjet(IProjet proj);

        void Remove(int ProjetId);
    }
}
