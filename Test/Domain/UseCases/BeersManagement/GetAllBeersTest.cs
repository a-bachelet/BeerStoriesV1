using System.Collections.Generic;
using System.Linq;
using Domain.BeersManagement.Models;
using Domain.BeersManagement.Services;
using Domain.BeersManagement.UseCases;
using Domain.BeersManagement.UseCases.Interfaces;
using Moq;
using Xunit;

namespace Test.Domain.UseCases.BeersManagement
{
    public class GetAllBeersTest : IGetAllBersPresenter
    {
        private readonly IBeerCatalog _catalog;

        public GetAllBeersTest()
        {
            var catalogMock = new Mock<IBeerCatalog>();

            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            catalogMock.Setup(c => c.FindAllBeers()).Returns(new List<Beer>
            {
                new Beer(null, beerLabel, beerDescription, beerStock),
                new Beer(null, beerLabel, beerDescription, beerStock)
            }.AsQueryable());

            _catalog = catalogMock.Object;
        }

        private GetAllBeersResponse Response { get; set; }

        void IGetAllBersPresenter.Present(GetAllBeersResponse response)
        {
            Response = response;
        }

        [Theory]
        [InlineData(1, 50)]
        [InlineData(null, null)]
        public void ItShouldReturnAllTheBeers(int? page, int? perPage)
        {
            var request = new GetAllBeersRequest
            {
                Page = page,
                PerPage = perPage
            };

            var getAllBeersUseCase = new GetAllBeers(_catalog);

            getAllBeersUseCase.Execute(request, this);

            Assert.Equal(2, Response.Beers.Count);
        }
    }
}