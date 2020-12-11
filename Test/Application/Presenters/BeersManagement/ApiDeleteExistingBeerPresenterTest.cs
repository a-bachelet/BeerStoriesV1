using Application.Presenters.BeersManagement;
using Domain.BeersManagement.UseCases;
using Xunit;

namespace Test.Application.Presenters.BeersManagement
{
    public class ApiDeleteExistingBeerTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ItShouldReturn204HttpCode(bool founded)
        {
            var response = new DeleteExistingBeerResponse
            {
                Founded = founded
            };

            var presenter = new ApiDeleteExistingBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Equal(204, viewModel.HttpCode);
        }
    }
}