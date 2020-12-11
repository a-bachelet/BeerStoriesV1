using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases.Interfaces;
using Domain.BeersManagement.Validators;

namespace Domain.BeersManagement.UseCases
{
    public class UpdateExistingBeer : IUpdateExistingBeer
    {
        private readonly IBeerCatalog _catalog;

        public UpdateExistingBeer(IBeerCatalog catalog)
        {
            _catalog = catalog;
        }

        public void Execute(UpdateExistingBeerRequest request, IUpdateExistingBeerPresenter presenter)
        {
            var response = new UpdateExistingBeerResponse();

            var validator = new UpdateExistingBeerValidator(_catalog);

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                response.Errors = validationResult.Errors;

                presenter.Present(response);
            }
            else
            {
                var beer = new Beer(request.Id, request.Label, request.Description, request.Stock);

                response.Beer = _catalog.UpdateExistingBeer(beer);

                presenter.Present(response);
            }
        }
    }
}