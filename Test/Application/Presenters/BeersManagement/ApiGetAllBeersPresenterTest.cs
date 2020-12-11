using System;
using System.Collections.Generic;
using System.Linq;
using Application.Presenters.BeersManagement;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.UseCases;
using Xunit;

namespace Test.Application.Presenters.BeersManagement
{
    public class ApiGetAllBeersPresenterTest
    {
        [Fact]
        public void ItShouldReturn200HttpCode()
        {
            var response = new GetAllBeersResponse
            {
                Beers = new List<Beer>
                {
                    new Beer(Guid.NewGuid(), "Label", "Description", 0)
                }
            };

            var presenter = new ApiGetAllBeersPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Equal(200, viewModel.HttpCode);
        }

        [Fact]
        public void ItShouldReturn204HttpCode()
        {
            var response = new GetAllBeersResponse
            {
                Beers = new List<Beer>()
            };

            var presenter = new ApiGetAllBeersPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Equal(204, viewModel.HttpCode);
        }

        [Fact]
        public void ItShouldReturnOneAvailableBeer()
        {
            var response = new GetAllBeersResponse
            {
                Beers = new List<Beer>
                {
                    new Beer(Guid.NewGuid(), "Label", "Description", 250),
                    new Beer(Guid.NewGuid(), "Label", "Description", 0),
                    new Beer(Guid.NewGuid(), "Label", "Description", 0)
                }
            };

            var presenter = new ApiGetAllBeersPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            var availableBeers = viewModel.Data.Where(b => b.Available).ToList();

            Assert.Single(availableBeers);
        }

        [Fact]
        public void ItShouldReturnOneLastItemsBeer()
        {
            var response = new GetAllBeersResponse
            {
                Beers = new List<Beer>
                {
                    new Beer(Guid.NewGuid(), "Label", "Description", 5),
                    new Beer(Guid.NewGuid(), "Label", "Description", 0),
                    new Beer(Guid.NewGuid(), "Label", "Description", 0)
                }
            };

            var presenter = new ApiGetAllBeersPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            var availableBeers = viewModel.Data.Where(b => b.LastItems).ToList();

            Assert.Single(availableBeers);
        }

        [Fact]
        public void ItShouldReturnTheSameData()
        {
            var response = new GetAllBeersResponse
            {
                Beers = new List<Beer>
                {
                    new Beer(Guid.NewGuid(), "Label", "Description", 100)
                }
            };

            var presenter = new ApiGetAllBeersPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            var responseBeers = response.Beers.ToList();
            var viewModelBeers = viewModel.Data.ToList();

            Assert.Equal(viewModelBeers[0].Id, responseBeers[0].Id.Value);
            Assert.Equal(viewModelBeers[0].Label, responseBeers[0].Label.Value);
            Assert.Equal(viewModelBeers[0].Description, responseBeers[0].Description.Value);
            Assert.Equal(viewModelBeers[0].Stock, responseBeers[0].Stock.Value);
        }
    }
}