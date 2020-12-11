using System;
using System.Linq;
using Domain.BeersManagement.Models;

namespace Domain.BeersManagement.Services
{
    public interface IBeerCatalog
    {
        IQueryable<Beer> FindAllBeers();

        Beer FindOneBeerByGuid(Guid id);

        Beer CreateNewBeer(Beer beer);

        Beer UpdateExistingBeer(Beer beer);

        void DeleteExistingBeer(Guid id);
    }
}