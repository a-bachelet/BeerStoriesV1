using Application.ViewModels.BeersManagement;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;

namespace Application.Presenters.BeersManagement
{
    public class ApiGetOneBeerPresenter : IGetOneBeerPresenter
    {
        public ApiGetOneBeerViewModel ViewModel { get; private set; }

        public void Present(GetOneBeerResponse response)
        {
            var vmHttpCode = response.Beer != null ? 200 : 404;

            var vmData = response.Beer != null
                ? new ApiGetOneBeerViewModel.Beer
                {
                    Id = response.Beer.Id.Value,
                    Label = response.Beer.Label.Value,
                    Description = response.Beer.Description.Value,
                    Stock = response.Beer.Stock.Value,
                    Available = response.Beer.Stock.Value > 0,
                    LastItems = response.Beer.Stock.Value > 0 && response.Beer.Stock.Value <= 10
                }
                : null;

            ViewModel = new ApiGetOneBeerViewModel
            {
                HttpCode = vmHttpCode,
                Data = vmData
            };
        }
    }
}