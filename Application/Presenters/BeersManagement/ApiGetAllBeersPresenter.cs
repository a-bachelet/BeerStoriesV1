using System.Linq;
using Application.ViewModels.BeersManagement;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;

namespace Application.Presenters.BeersManagement
{
    public class ApiGetAllBeersPresenter : IGetAllBersPresenter
    {
        public ApiGetAllBeersViewModel ViewModel { get; private set; }

        public void Present(GetAllBeersResponse response)
        {
            var vmHttpCode = response.Beers.Count > 0 ? 200 : 204;

            var vmData = response.Beers.Select(b => new ApiGetAllBeersViewModel.Beer
            {
                Id = b.Id.Value,
                Label = b.Label.Value,
                Description = b.Description.Value,
                Stock = b.Stock.Value,
                Available = b.Stock.Value > 0,
                LastItems = b.Stock.Value > 0 && b.Stock.Value <= 10
            }).ToList();

            ViewModel = new ApiGetAllBeersViewModel
            {
                HttpCode = vmHttpCode,
                Data = vmData
            };
        }
    }
}