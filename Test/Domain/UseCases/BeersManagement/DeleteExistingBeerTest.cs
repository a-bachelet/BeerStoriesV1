using System;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;
using Moq;
using Xunit;

namespace Test.Domain.UseCases.BeersManagement
{
    public class DeleteExistingBeerTest : IDeleteExistingBeerPresenter
    {
        private DeleteExistingBeerResponse Response { get; set; }

        void IDeleteExistingBeerPresenter.Present(DeleteExistingBeerResponse response)
        {
            Response = response;
        }

        [Fact]
        public void ItShouldReturnFoundedIfTheIdIsFound()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>()))
                .Returns((Guid id) => new Beer(id, "Label", "Description", 0));

            var request = new DeleteExistingBeerRequest
            {
                Id = Guid.NewGuid()
            };

            var deleteExistingBeerUseCase = new DeleteExistingBeer(catalogMock.Object);

            deleteExistingBeerUseCase.Execute(request, this);

            Assert.True(Response.Founded);
        }

        [Fact]
        public void ItShouldReturnNotFoundedIfTheIdIsNotFound()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>())).Returns((Guid id) => null);

            var request = new DeleteExistingBeerRequest
            {
                Id = Guid.NewGuid()
            };

            var deleteExistingBeerUseCase = new DeleteExistingBeer(catalogMock.Object);

            deleteExistingBeerUseCase.Execute(request, this);

            Assert.False(Response.Founded);
        }
    }
}