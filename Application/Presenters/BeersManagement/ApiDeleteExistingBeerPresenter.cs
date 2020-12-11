using Application.ViewModels.BeersManagement;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;

namespace Application.Presenters.BeersManagement
{
    public class ApiDeleteExistingBeerPresenter : IDeleteExistingBeerPresenter
    {
        public ApiDeleteExistingBeerViewModel ViewModel { get; private set; }

        public void Present(DeleteExistingBeerResponse response)
        {
            const int vmHttpCode = 204;

            ViewModel = new ApiDeleteExistingBeerViewModel
            {
                HttpCode = vmHttpCode
            };
        }
    }
}