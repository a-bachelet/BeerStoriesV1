using System;
using System.Collections.Generic;
using System.Linq;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;

namespace Infrastructure.BeersManagement
{
    public class BeerCatalogFake : IBeerCatalog
    {
        private readonly ICollection<Beer> _beers;

        public BeerCatalogFake()
        {
            _beers = new List<Beer>();
        }

        public IQueryable<Beer> FindAllBeers()
        {
            return _beers.AsQueryable();
        }

        public Beer FindOneBeerByGuid(Guid id)
        {
            return _beers.FirstOrDefault(b => b.Id.Value == id);
        }

        public Beer CreateNewBeer(Beer beer)
        {
            _beers.Add(beer);

            return beer;
        }

        public Beer UpdateExistingBeer(Beer beer)
        {
            var oldBeer = FindOneBeerByGuid(beer.Id.Value);

            if (oldBeer == null) return null;

            _beers.Remove(oldBeer);

            _beers.Add(beer);

            return beer;
        }

        public void DeleteExistingBeer(Guid id)
        {
            var beer = FindOneBeerByGuid(id);

            if (beer != null) _beers.Remove(beer);
        }
    }
}