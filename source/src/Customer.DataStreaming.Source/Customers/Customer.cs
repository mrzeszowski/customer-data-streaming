using Bogus;

namespace Customer.DataStreaming.Source.Customers;

public class Customer
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyList<CommunicationChannel> CommunicationChannels { get; private set; }
    public IReadOnlyList<Consent> Consents { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public long Version { get; private set; }

    private Customer() { }
    
    public Customer(string firstName,
        string lastName,
        string email,
        DateTime dateOfBirth,
        Address address,
        IReadOnlyList<CommunicationChannel> communicationChannels,
        IReadOnlyList<Consent> consents, 
        DateTime createdAt,
        DateTime? updatedAt = null,
        long version = 1)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
        Address = address;
        CommunicationChannels = communicationChannels.ToList();
        Consents = consents.ToList();
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Version = version;
    }

    public void Update()
    {
        var customer = new CustomerFaker().Generate();
        Address = customer.Address;
        CommunicationChannels = customer.CommunicationChannels;
        Consents = customer.Consents;
        UpdatedAt = DateTime.UtcNow;
        Version++;
    }
}

public sealed class CustomerFaker : Faker<Customer>
{
    public CustomerFaker()
    {
        var addressFaker = new AddressFaker();
        var communicationChannelFaker = new CommunicationChannelFaker();
        var consentFaker = new ConsentFaker();

        CustomInstantiator(f => new Customer(
            firstName: f.Name.FirstName(),
            lastName: f.Name.LastName(),
            email: f.Internet.Email(),
            dateOfBirth: f.Date.Past(40, DateTime.UtcNow.AddYears(-18)), // 18-58 years old
            address: addressFaker.Generate(),
            communicationChannels: communicationChannelFaker.Generate(3),
            consents: consentFaker.Generate(2),
            createdAt: DateTime.UtcNow
        ));
    }
}