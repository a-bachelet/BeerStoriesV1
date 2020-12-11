using System;
using System.Collections.Generic;
using System.Linq;
using Application.Controllers.BeersManagement;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test.Application.Controllers.BeersManagement
{
    public class ApiBeerControllerTest
    {
        [Fact]
        public void GetAllBeersShouldReturnOkObjectResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindAllBeers()).Returns(() =>
                new List<Beer>
                {
                    new Beer(Guid.NewGuid(), "Label", "Description", 0)
                }.AsQueryable()
            );

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.GetAllBeers(new GetAllBeersRequest {Page = 1, PerPage = 50});

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllBeersShouldReturnNoContentResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindAllBeers())
                .Returns(() => new List<Beer>().AsQueryable());

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.GetAllBeers(new GetAllBeersRequest {Page = 1, PerPage = 50});

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetOneBeerShouldReturnOkObjectResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>()))
                .Returns((Guid id) => new Beer(id, "Label", "Description", 0));

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.GetOneBeer(new GetOneBeerRequest {Id = Guid.NewGuid()});

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetOneBeerShouldReturnNotFoundResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>())).Returns((Guid id) => null);

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.GetOneBeer(new GetOneBeerRequest {Id = Guid.NewGuid()});

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CreateNewBeerShouldReturnCreatedResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.CreateNewBeer(It.IsAny<Beer>()))
                .Returns((Beer beer) => beer);

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.CreateNewBeer(new CreateNewBeerRequest
            {
                Label = "Sample Label",
                Description = "Sample Description",
                Stock = 250
            });

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public void CreateNewBeerShouldReturnBadRequestObjectResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.CreateNewBeer(It.IsAny<Beer>()))
                .Returns((Beer beer) => beer);

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.CreateNewBeer(new CreateNewBeerRequest
            {
                Label = "Sa",
                Description = "Sa",
                Stock = -5
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateExistingBeerShouldReturnOkObjectResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>()))
                .Returns((Guid id) => new Beer(id, "Sample Label", "Sample Description", 250));

            catalogMock.Setup(c => c.UpdateExistingBeer(It.IsAny<Beer>()))
                .Returns((Beer beer) => beer);

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.UpdateExistingBeer(new UpdateExistingBeerRequest
            {
                Id = Guid.NewGuid(),
                Label = "Sample Label",
                Description = "Sample Description",
                Stock = 250
            });

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateExistingBeerShouldReturnBadRequestObjectResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.UpdateExistingBeer(It.IsAny<Beer>()))
                .Returns((Beer beer) => beer);

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var result = controller.UpdateExistingBeer(new UpdateExistingBeerRequest
            {
                Id = Guid.NewGuid(),
                Label = "Sa",
                Description = "Sa",
                Stock = -5
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteExistingBeerShouldReturnNoContentResult()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            var catalog = catalogMock.Object;

            var controller = new ApiBeerController(catalog);

            var resultWithId = controller.DeleteExistingBeer(new DeleteExistingBeerRequest {Id = Guid.NewGuid()});
            var resultWithoutId = controller.DeleteExistingBeer(new DeleteExistingBeerRequest());

            Assert.IsType<NoContentResult>(resultWithId);
            Assert.IsType<NoContentResult>(resultWithoutId);
        }
    }
}