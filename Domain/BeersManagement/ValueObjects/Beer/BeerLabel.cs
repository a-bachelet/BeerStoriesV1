using Domain.BeersManagement.Exceptions;

namespace Domain.BeersManagement.ValueObjects.Beer
{
    public class BeerLabel
    {
        public BeerLabel(string value)
        {
            if (value == null) throw new ValidationException("A beer label cannot be null.");

            if (value.Length < 3) throw new ValidationException("A beer label should be at least 3 characters long.");

            Value = value;
        }

        public string Value { get; }
    }
}