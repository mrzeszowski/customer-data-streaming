using Bogus;

namespace Customer.DataStreaming.Source.Customers;

public sealed class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }

    public Address() { }
    
    public Address(string street, string city, string state, string postalCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }
}

public sealed class AddressFaker : Faker<Address>
{
    public AddressFaker()
    {
        RuleFor(x => x.Street, f => f.Address.StreetAddress());
        RuleFor(x => x.City, f => f.Address.City());
        RuleFor(x => x.State, f => f.Address.State());
        RuleFor(x => x.PostalCode, f => f.Address.ZipCode());
        RuleFor(x => x.Country, f => f.Address.Country());
    }
}