using System;
using Domain.BeersManagement.Exceptions;
using Domain.BeersManagement.Models;
using Xunit;

namespace Test.Domain.Models.BeersManagement
{
    public class BeerTest
    {
        [Fact]
        public void ItShouldBeABeer()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;


            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            Assert.IsType<Beer>(beer);
        }

        [Fact]
        public void ItShouldGenerateANewGuid()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            Assert.IsType<Guid>(beer.Id.Value);
        }

        [Fact]
        public void ItShouldKeepTheSameGuid()
        {
            var beerId = Guid.NewGuid();
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(beerId, beerLabel, beerDescription, beerStock);

            Assert.IsType<Guid>(beer.Id.Value);
            Assert.Equal(beerId, beer.Id.Value);
        }

        [Fact]
        public void ItShouldThrowAnExceptionWithNullLabel()
        {
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            void Action()
            {
                var unused = new Beer(null, null, beerDescription, beerStock);
            }

            Assert.Equal(
                "A beer label cannot be null.",
                Assert.Throws<ValidationException>(Action).Message
            );
        }

        [Fact]
        public void ItShouldThrowAnExceptionWithTooShortLabel()
        {
            const string beerLabel = "Sa";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            void Action()
            {
                var unused = new Beer(null, beerLabel, beerDescription, beerStock);
            }

            Assert.Equal(
                "A beer label should be at least 3 characters long.",
                Assert.Throws<ValidationException>(Action).Message
            );
        }

        [Fact]
        public void ItShouldKeepTheSameLabel()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            Assert.Equal(beerLabel, beer.Label.Value);
        }

        [Fact]
        public void ItShouldThrowAnExceptionWithNullDescription()
        {
            const string beerLabel = "Sample Label";
            const int beerStock = 250;

            void Action()
            {
                var unused = new Beer(null, beerLabel, null, beerStock);
            }

            Assert.Equal(
                "A beer description cannot be null.",
                Assert.Throws<ValidationException>(Action).Message
            );
        }

        [Fact]
        public void ItShouldThrowAnExceptionWithTooShortDescription()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sa";
            const int beerStock = 250;

            void Action()
            {
                var unused = new Beer(null, beerLabel, beerDescription, beerStock);
            }

            Assert.Equal(
                "A beer description should be at least 3 characters long.",
                Assert.Throws<ValidationException>(Action).Message
            );
        }

        [Fact]
        public void ItShouldKeepTheSameDescription()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            Assert.Equal(beerDescription, beer.Description.Value);
        }

        [Fact]
        public void ItShouldThrowAnExceptionWithNullStock()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";

            void Action()
            {
                var unused = new Beer(null, beerLabel, beerDescription, null);
            }

            Assert.Equal(
                "A beer stock cannot be null.",
                Assert.Throws<ValidationException>(Action).Message
            );
        }

        [Fact]
        public void ItShouldThrowAnExceptionWithNegativeStock()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = -5;

            void Action()
            {
                var unused = new Beer(null, beerLabel, beerDescription, beerStock);
            }

            Assert.Equal(
                "A beer stock cannot have a negative value.",
                Assert.Throws<ValidationException>(Action).Message
            );
        }

        [Fact]
        public void ItShouldKeepTheSameStock()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            Assert.Equal(250, beer.Stock.Value);
        }

        [Fact]
        public void ItShouldIncreaseStockWithPositiveValue()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            beer.IncreaseStock(5);

            Assert.Equal(255, beer.Stock.Value);
        }

        [Fact]
        public void ItShouldIncreaseStockWithNegativeValue()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            beer.IncreaseStock(-5);

            Assert.Equal(255, beer.Stock.Value);
        }

        [Fact]
        public void ItShouldDecreaseStockWithPositiveValue()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            beer.DecreaseStock(5);

            Assert.Equal(245, beer.Stock.Value);
        }

        [Fact]
        public void ItShouldDecreaseStockWithNegativeValue()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            beer.DecreaseStock(-5);

            Assert.Equal(245, beer.Stock.Value);
        }

        [Fact]
        public void ItShouldNotDecreaseStockUnderZero()
        {
            const string beerLabel = "Sample Label";
            const string beerDescription = "Sample Description";
            const int beerStock = 250;

            var beer = new Beer(null, beerLabel, beerDescription, beerStock);

            beer.DecreaseStock(300);

            Assert.Equal(0, beer.Stock.Value);
        }
    }
}