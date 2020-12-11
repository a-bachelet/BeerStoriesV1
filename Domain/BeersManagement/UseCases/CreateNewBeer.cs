using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases.Interfaces;
using Domain.BeersManagement.Validators;

namespace Domain.BeersManagement.UseCases
{
    public class CreateNewBeer : ICreateNewBeer
    {
        private readonly IBeerCatalog _catalog;

        public CreateNewBeer(IBeerCatalog catalog)
        {
            _catalog = catalog;
        }

        public void Execute(CreateNewBeerRequest request, ICreateNewBeerPresenter presenter)
        {
            var response = new CreateNewBeerResponse();

            var validator = new CreateNewBeerValidator();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                response.Errors = validationResult.Errors;

                presenter.Present(response);
            }
            else
            {
                var beer = new Beer(null, request.Label, request.Description, request.Stock);

                response.Beer = _catalog.CreateNewBeer(beer);

                presenter.Present(response);
            }
        }
    }
}