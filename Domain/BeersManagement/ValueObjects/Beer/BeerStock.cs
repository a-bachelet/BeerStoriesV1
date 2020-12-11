using Domain.BeersManagement.Exceptions;

namespace Domain.BeersManagement.ValueObjects.Beer
{
    public class BeerStock
    {
        public BeerStock(int? value)
        {
            if (value == null) throw new ValidationException("A beer stock cannot be null.");

            if (value < 0) throw new ValidationException("A beer stock cannot have a negative value.");

            Value = (int) value;
        }

        public int Value { get; }
    }
}