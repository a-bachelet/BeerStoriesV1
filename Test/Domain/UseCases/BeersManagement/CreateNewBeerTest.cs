using System;
using System.Linq;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;
using Moq;
using Xunit;

namespace Test.Domain.UseCases.BeersManagement
{
    public class CreateNewBeerTest : ICreateNewBeerPresenter
    {
        private readonly IBeerCatalog _catalog;

        public CreateNewBeerTest()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.CreateNewBeer(It.IsAny<Beer>())).Returns((Beer beer) => beer);

            _catalog = catalogMock.Object;
        }

        private CreateNewBeerResponse Response { get; set; }

        void ICreateNewBeerPresenter.Present(CreateNewBeerResponse response)
        {
            Response = response;
        }


        [Fact]
        public void ItShouldReturnANewBeer()
        {
            var request = new CreateNewBeerRequest
            {
                Label = "Sample Label",
                Description = "Sample Description",
                Stock = 0
            };

            var createNewBeerUseCase = new CreateNewBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.IsType<Beer>(Response.Beer);
            Assert.IsType<Guid>(Response.Beer.Id.Value);
            Assert.Equal("Sample Label", Response.Beer.Label.Value);
            Assert.Equal("Sample Description", Response.Beer.Description.Value);
            Assert.Equal(0, Response.Beer.Stock.Value);
            Assert.Null(Response.Errors);
        }

        [Fact]
        public void ItShouldReturnAnErrorWithTooShortLabel()
        {
            var request = new CreateNewBeerRequest
            {
                Label = "Sa",
                Description = "Sample Description",
                Stock = 0
            };

            var createNewBeerUseCase = new CreateNewBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);
            Assert.Equal(1, Response.Errors.Count);
            Assert.Equal("Label", Response.Errors.FirstOrDefault().PropertyName);
        }

        [Fact]
        public void ItShouldReturnAnErrorWithTooShortDescription()
        {
            var request = new CreateNewBeerRequest
            {
                Label = "Sample Label",
                Description = "Sa",
                Stock = 0
            };

            var createNewBeerUseCase = new CreateNewBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);
            Assert.Equal(1, Response.Errors.Count);
            Assert.Equal("Description", Response.Errors.FirstOrDefault().PropertyName);
        }

        [Fact]
        public void ItShouldReturnAnErrorWithNegativeStock()
        {
            var request = new CreateNewBeerRequest
            {
                Label = "Sample Label",
                Description = "Sample Description",
                Stock = -1
            };

            var createNewBeerUseCase = new CreateNewBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);
            Assert.Equal(1, Response.Errors.Count);
            Assert.Equal("Stock", Response.Errors.FirstOrDefault().PropertyName);
        }

        [Fact]
        public void ItShouldReturnMultipleErrorsWithBadProperties()
        {
            var request = new CreateNewBeerRequest
            {
                Label = "Sa",
                Description = "Sa",
                Stock = -1
            };

            var createNewBeerUseCase = new CreateNewBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);

            var badProperties = Response.Errors.Select(e => e.PropertyName).ToList();

            Assert.Equal(3, badProperties.Count);
            Assert.Contains("Label", badProperties);
            Assert.Contains("Description", badProperties);
            Assert.Contains("Stock", badProperties);
        }
    }
}