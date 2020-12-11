using System;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;
using Moq;
using Xunit;

namespace Test.Domain.UseCases.BeersManagement
{
    public class GetOneBeerTest : IGetOneBeerPresenter
    {
        private IBeerCatalog _catalog;

        private GetOneBeerResponse Response { get; set; }

        void IGetOneBeerPresenter.Present(GetOneBeerResponse response)
        {
            Response = response;
        }

        [Fact]
        public void ItShouldReturnRequestedBeer()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>())).Returns(
                (Guid Id) => new Beer(Id, "Sample Label", "Sample Description", 0)
            );

            _catalog = catalogMock.Object;

            var id = Guid.NewGuid();

            var request = new GetOneBeerRequest
            {
                Id = id
            };

            var getOneBeerUseCase = new GetOneBeer(_catalog);

            getOneBeerUseCase.Execute(request, this);

            Assert.Equal(id, Response.Beer.Id.Value);
        }

        [Fact]
        public void ItShouldNotReturnRequestedBeer()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            catalogMock.Setup(c => c.FindOneBeerByGuid(It.IsAny<Guid>())).Returns(
                (Guid Id) => null
            );

            _catalog = catalogMock.Object;

            var id = Guid.NewGuid();

            var request = new GetOneBeerRequest
            {
                Id = id
            };

            var getOneBeerUseCase = new GetOneBeer(_catalog);

            getOneBeerUseCase.Execute(request, this);

            Assert.Null(Response.Beer);
        }
    }
}