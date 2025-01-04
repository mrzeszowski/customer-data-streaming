using Bogus;

namespace Customer.DataStreaming.Source.Customers;

public sealed class Consent
{
    public string Type { get; private set; }
    public bool IsGranted { get; private set; }
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

    public Consent() { }
    
    public Consent(string type, bool isGranted)
    {
        Type = type;
        IsGranted = isGranted;
    }
}

public sealed class ConsentFaker : Faker<Consent>
{
    public ConsentFaker()
    {
        CustomInstantiator(f => new Consent(
            type: f.PickRandom("Marketing", "Analytics", "Promotions", "Notifications"),
            isGranted: f.Random.Bool() 
        ));
    }
}