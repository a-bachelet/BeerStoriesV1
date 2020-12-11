using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases.Interfaces;

namespace Domain.BeersManagement.UseCases
{
    public class GetOneBeer : IGetOneBeer
    {
        private readonly IBeerCatalog _catalog;

        public GetOneBeer(IBeerCatalog catalog)
        {
            _catalog = catalog;
        }

        public void Execute(GetOneBeerRequest request, IGetOneBeerPresenter presenter)
        {
            var beer = _catalog.FindOneBeerByGuid(request.Id);

            var response = new GetOneBeerResponse
            {
                Beer = beer
            };

            presenter.Present(response);
        }
    }
}