using System.Linq;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases.Interfaces;

namespace Domain.BeersManagement.UseCases
{
    public class GetAllBeers : IGetAllBeers
    {
        private readonly IBeerCatalog _catalog;

        public GetAllBeers(IBeerCatalog catalog)
        {
            _catalog = catalog;
        }

        public void Execute(GetAllBeersRequest request, IGetAllBersPresenter presenter)
        {
            var page = request.Page ?? 1;
            var perPage = request.PerPage ?? 50;
            
            var beers = _catalog
                .FindAllBeers()
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            var response = new GetAllBeersResponse
            {
                Beers = beers
            };

            presenter.Present(response);
        }
    }
}