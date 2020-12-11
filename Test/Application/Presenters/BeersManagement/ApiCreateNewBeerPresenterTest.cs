using System;
using System.Collections.Generic;
using System.Linq;
using Application.Presenters.BeersManagement;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.UseCases;
using FluentValidation.Results;
using Xunit;

namespace Test.Application.Presenters.BeersManagement
{
    public class ApiCreateNewBeerPresenterTest
    {
        [Fact]
        public void ItShouldReturn201HttpCode()
        {
            var response = new CreateNewBeerResponse
            {
                Beer = new Beer(Guid.NewGuid(), "Label", "Description", 0),
                Errors = null
            };

            var presenter = new ApiCreateNewBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Equal(201, viewModel.HttpCode);
        }

        [Fact]
        public void ItShouldReturnCreatedBeer()
        {
            var responseBeer = new Beer(Guid.NewGuid(), "Label", "Description", 5);

            var response = new CreateNewBeerResponse
            {
                Beer = responseBeer,
                Errors = null
            };

            var presenter = new ApiCreateNewBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            var viewModelBeer = viewModel.Data;

            Assert.Equal(responseBeer.Id.Value, viewModelBeer.Id);
            Assert.Equal(responseBeer.Label.Value, viewModelBeer.Label);
            Assert.Equal(responseBeer.Description.Value, viewModelBeer.Description);
            Assert.Equal(responseBeer.Stock.Value, viewModelBeer.Stock);
            Assert.True(viewModelBeer.Available);
            Assert.True(viewModelBeer.LastItems);
        }

        [Fact]
        public void ItShouldNotReturnErrors()
        {
            var response = new CreateNewBeerResponse
            {
                Beer = new Beer(Guid.NewGuid(), "Label", "Description", 5),
                Errors = new List<ValidationFailure>()
            };

            var presenter = new ApiCreateNewBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Null(viewModel.Errors);
        }

        [Fact]
        public void ItShouldReturn400HttpCode()
        {
            var response = new CreateNewBeerResponse
            {
                Beer = null,
                Errors = null
            };

            var presenter = new ApiCreateNewBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Equal(400, viewModel.HttpCode);
        }

        [Fact]
        public void ItShouldNotReturnCreatedBeer()
        {
            var response = new CreateNewBeerResponse
            {
                Beer = null,
                Errors = null
            };

            var presenter = new ApiCreateNewBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            Assert.Null(viewModel.Data);
        }

        [Fact]
        public void ItShouldReturnErrors()
        {
            var responsErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Label", "Label Error"),
                new ValidationFailure("Description", "Description Error")
            };

            var response = new CreateNewBeerResponse
            {
                Beer = null,
                Errors = responsErrors
            };

            var presenter = new ApiCreateNewBeerPresenter();

            presenter.Present(response);

            var viewModel = presenter.ViewModel;

            var viewModelErrors = viewModel.Errors?.Select(e => e).ToList();

            Assert.NotNull(viewModelErrors);
            Assert.Equal(2, viewModelErrors.Count);
            Assert.Equal(responsErrors[0].PropertyName, viewModelErrors[0].Key);
            Assert.Equal(responsErrors[0].ErrorMessage, viewModelErrors[0].Value);
            Assert.Equal(responsErrors[1].PropertyName, viewModelErrors[1].Key);
            Assert.Equal(responsErrors[1].ErrorMessage, viewModelErrors[1].Value);
        }
    }
}