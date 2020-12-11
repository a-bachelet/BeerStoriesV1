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
    public class UpdateExistingBeerTest : IUpdateExistingBeerPresenter
    {
        private readonly IBeerCatalog _catalog;
        private Mock<IBeerCatalog> _catalogMock;

        public UpdateExistingBeerTest()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.UpdateExistingBeer(It.IsAny<Beer>())).Returns((Beer beer) => beer);
            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>()))
                .Returns((Guid Id) => new Beer(null, "Label", "Description", 0));

            _catalogMock = catalogMock;
            _catalog = catalogMock.Object;
        }

        private UpdateExistingBeerResponse Response { get; set; }

        void IUpdateExistingBeerPresenter.Present(UpdateExistingBeerResponse response)
        {
            Response = response;
        }


        [Fact]
        public void ItShouldReturnTheUpdatedBeer()
        {
            var request = new UpdateExistingBeerRequest
            {
                Id = Guid.NewGuid(),
                Label = "Sample Label",
                Description = "Sample Description",
                Stock = 0
            };

            var updateExistingBeerUseCase = new UpdateExistingBeer(_catalog);

            updateExistingBeerUseCase.Execute(request, this);

            Assert.IsType<Beer>(Response.Beer);
            Assert.IsType<Guid>(Response.Beer.Id.Value);
            Assert.Equal("Sample Label", Response.Beer.Label.Value);
            Assert.Equal("Sample Description", Response.Beer.Description.Value);
            Assert.Equal(0, Response.Beer.Stock.Value);
            Assert.Null(Response.Errors);
        }

        [Fact]
        public void ItShouldReturnAnErrorWithUnknownGuid()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.UpdateExistingBeer(It.IsAny<Beer>())).Returns((Beer beer) => beer);
            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>())).Returns((Guid Id) => null);

            var request = new UpdateExistingBeerRequest
            {
                Id = Guid.NewGuid(),
                Label = "Sample Description",
                Description = "Sample Description",
                Stock = 0
            };

            var updateExistingBeerUseCase = new UpdateExistingBeer(catalogMock.Object);

            updateExistingBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);
            Assert.Equal(1, Response.Errors.Count);
            Assert.Equal("Id", Response.Errors.FirstOrDefault().PropertyName);
        }

        [Fact]
        public void ItShouldReturnAnErrorWithTooShortLabel()
        {
            var request = new UpdateExistingBeerRequest
            {
                Label = "Sa",
                Description = "Sample Description",
                Stock = 0
            };

            var createNewBeerUseCase = new UpdateExistingBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);
            Assert.Equal(1, Response.Errors.Count);
            Assert.Equal("Label", Response.Errors.FirstOrDefault().PropertyName);
        }

        [Fact]
        public void ItShouldReturnAnErrorWithTooShortDescription()
        {
            var request = new UpdateExistingBeerRequest
            {
                Label = "Sample Label",
                Description = "Sa",
                Stock = 0
            };

            var createNewBeerUseCase = new UpdateExistingBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);
            Assert.Equal(1, Response.Errors.Count);
            Assert.Equal("Description", Response.Errors.FirstOrDefault().PropertyName);
        }

        [Fact]
        public void ItShouldReturnAnErrorWithNegativeStock()
        {
            var request = new UpdateExistingBeerRequest
            {
                Label = "Sample Label",
                Description = "Sample Description",
                Stock = -1
            };

            var createNewBeerUseCase = new UpdateExistingBeer(_catalog);

            createNewBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
            Assert.NotNull(Response.Errors);
            Assert.Equal(1, Response.Errors.Count);
            Assert.Equal("Stock", Response.Errors.FirstOrDefault().PropertyName);
        }

        [Fact]
        public void ItShouldReturnMultipleErrorsWithBadProperties()
        {
            var request = new UpdateExistingBeerRequest
            {
                Label = "Sa",
                Description = "Sa",
                Stock = -1
            };

            var createNewBeerUseCase = new UpdateExistingBeer(_catalog);

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