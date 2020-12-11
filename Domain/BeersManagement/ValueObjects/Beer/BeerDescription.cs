using Domain.BeersManagement.Exceptions;

namespace Domain.BeersManagement.ValueObjects.Beer
{
    public class BeerDescription
    {
        public BeerDescription(string value)
        {
            if (value == null) throw new ValidationException("A beer description cannot be null.");

            if (value.Length < 3)
                throw new ValidationException("A beer description should be at least 3 characters long.");

            Value = value;
        }

        public string Value { get; }
    }
}