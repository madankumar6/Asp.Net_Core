
namespace Order.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!; 
        public string LastName { get; } = default!;
        public string Email { get; } = default!;
        public string AddressLine { get; } = default!;
        public string State { get; } = default!;
        public string Country { get; } = default!;
        public string ZipCode { get; } = default!;

        //For EF Core
        protected Address()
        {
        }

        // Add constructor for assigning all values
        public Address(string firstName, string lastName, string email, string addressLine, string state, string country, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AddressLine = addressLine;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public static Address Of(string firstName, string lastName, string email, string addressLine, string state, string country, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            return new Address(firstName, lastName, email, addressLine, state, country, zipCode);
        }
    }
}
