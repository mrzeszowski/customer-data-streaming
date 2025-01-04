using Bogus;

namespace Customer.DataStreaming.Source.Customers;

public sealed class CommunicationChannel
{
    public string Type { get; private set; }
    public string Value { get; private set; }
    public bool IsPrimary { get; private set; }

    private CommunicationChannel() { }

    public CommunicationChannel(string type, string value, bool isPrimary = false)
    {
        Type = type;
        Value = value;
        IsPrimary = isPrimary;
    }
}

public sealed class CommunicationChannelFaker : Faker<CommunicationChannel>
{
    public CommunicationChannelFaker()
    {
        CustomInstantiator(f =>
        {
            var type = f.PickRandom(new[] { "Email", "Phone", "SMS" });
            return new CommunicationChannel(
                type: type,
                value: GenerateValue(f, type),
                isPrimary: f.Random.Bool(0.2f)
            );
        });
    }
    
    private static string GenerateValue(Faker faker, string type)
    {
        return type switch
        {
            "Email" => faker.Internet.Email(),
            "Phone" => faker.Phone.PhoneNumber("+###########"),
            "SMS" => faker.Phone.PhoneNumber("+###########"),
            _ => throw new ArgumentException("Unknown type")
        };
    }
}