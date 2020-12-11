using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases.Interfaces;

namespace Domain.BeersManagement.UseCases
{
    public class DeleteExistingBeer : IDeleteExistingBeer
    {
        private readonly IBeerCatalog _catalog;

        public DeleteExistingBeer(IBeerCatalog catalog)
        {
            _catalog = catalog;
        }

        public void Execute(DeleteExistingBeerRequest request, IDeleteExistingBeerPresenter presenter)
        {
            var beer = _catalog.FindOneBeerByGuid(request.Id);

            var response = new DeleteExistingBeerResponse();

            if (beer == null)
            {
                response.Founded = false;
            }
            else
            {
                _catalog.DeleteExistingBeer(request.Id);

                response.Founded = true;
            }

            presenter.Present(response);
        }
    }
}