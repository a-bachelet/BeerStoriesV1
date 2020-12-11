using System;
using Domain.BeersManagement.Models.Interfaces;
using Domain.BeersManagement.ValueObjects.Beer;

namespace Domain.BeersManagement.Models
{
    public class Beer : IBeer
    {
        public Beer(Guid? id, string label, string description, int? stock)
        {
            Id = new BeerId(id);
            Label = new BeerLabel(label);
            Description = new BeerDescription(description);
            Stock = new BeerStock(stock);
        }

        public BeerId Id { get; }

        public BeerLabel Label { get; }

        public BeerDescription Description { get; }

        public BeerStock Stock { get; private set; }

        public void IncreaseStock(int stock)
        {
            Stock = new BeerStock(Stock.Value + Math.Abs(stock));
        }

        public void DecreaseStock(int stock)
        {
            var result = Stock.Value - Math.Abs(stock);

            Stock = new BeerStock(result < 0 ? 0 : result);
        }
    }
}