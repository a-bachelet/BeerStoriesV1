using System;
using Application.Presenters.BeersManagement;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.UseCases;
using Xunit;

namespace Test.Application.Presenters.BeersManagement
{
    public class ApiGetOneBeerPresenterTest
    {
        [Fact]
        public void ItShouldReturn200HttpCode()
        {
            var response = new GetOneBeerResponse
            {
                Beer = new Beer(Guid.NewGuid(), "Label", "Description", 0)
            };

            var presenter = new ApiGetOneBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Equal(200, viewModel.HttpCode);
        }

        [Fact]
        public void ItShouldReturn404HttpCode()
        {
            var response = new GetOneBeerResponse
            {
                Beer = null
            };

            var presenter = new ApiGetOneBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Equal(404, viewModel.HttpCode);
        }

        [Fact]
        public void ItShouldReturnAvailable()
        {
            var response = new GetOneBeerResponse
            {
                Beer = new Beer(Guid.NewGuid(), "Label", "Description", 250)
            };

            var presenter = new ApiGetOneBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.True(viewModel.Data.Available);
        }

        [Fact]
        public void ItShouldReturnLastItems()
        {
            var response = new GetOneBeerResponse
            {
                Beer = new Beer(Guid.NewGuid(), "Label", "Description", 5)
            };

            var presenter = new ApiGetOneBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.True(viewModel.Data.LastItems);
        }

        [Fact]
        public void ItShouldReturnTheSameData()
        {
            var response = new GetOneBeerResponse
            {
                Beer = new Beer(Guid.NewGuid(), "Label", "Description", 100)
            };

            var presenter = new ApiGetOneBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            var responseBeer = response.Beer;
            var viewModelBeer = viewModel.Data;

            Assert.Equal(viewModelBeer.Id, responseBeer.Id.Value);
            Assert.Equal(viewModelBeer.Label, responseBeer.Label.Value);
            Assert.Equal(viewModelBeer.Description, responseBeer.Description.Value);
            Assert.Equal(viewModelBeer.Stock, responseBeer.Stock.Value);
        }
    }
}