using Bogus;
using Customer.DataStreaming.Source.Customers;

namespace Customer.DataStreaming.Source.Transactions;

public class Transaction
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public IReadOnlyList<TransactionProduct> Products { get; private set; }
    public ShipmentDetails Shipment { get; private set; }
    public PaymentDetails Payment { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public long Version { get; private set; }

    private Transaction() { }
    
    public Transaction(Guid id,
        Guid customerId,
        IReadOnlyList<TransactionProduct> products,
        ShipmentDetails shipment,
        PaymentDetails payment,
        DateTime createdAt,
        DateTime? updatedAt = null,
        long version = 1)
    {
        Id = id;
        CustomerId = customerId;
        Products = products;
        Shipment = shipment;
        Payment = payment;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Version = version;
    }

    public static Transaction Create(Guid customerId, 
        IReadOnlyList<TransactionProduct> products,
        ShipmentDetails shipment, 
        PaymentDetails payment)
    {
        return new Transaction(Guid.NewGuid(), customerId, products, shipment, payment, DateTime.UtcNow);
    }

    public void Pay()
    {
        Payment.Pay();
        UpdatedAt = DateTime.UtcNow;
        Version++;
    }
}

public class TransactionProduct
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public TransactionProduct(Guid productId, string name, decimal price, int quantity)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

public class ShipmentDetails
{
    public string Address { get; private set; }
    public string Provider { get; private set; }

    public static string[] Providers = new[]
    {
        "FedEx", "UPS", "DHL", "InPost"
    };
    
    public ShipmentDetails(string address, string provider)
    {
        Address = address;
        Provider = provider;
    }

    public static ShipmentDetails Create(Address address)
    {
        return new ShipmentDetails(
            address: $"{address.Street} | {address.PostalCode}, {address.City} | {address.State} | {address.Country}",
            provider: GetProvider());
    }

    private static string GetProvider() => new Faker().PickRandom(Providers);
}

public class PaymentDetails
{
    public string Type { get; private set; }
    public decimal Amount { get; private set; }
    public bool IsSuccessful { get; private set; }

    public static string[] Types = new[]
    {
        "CreditCard", "DebitCard", "PayPal"
    };
    
    public PaymentDetails(string type, decimal amount, bool isSuccessful)
    {
        Type = type;
        Amount = amount;
        IsSuccessful = isSuccessful;
    }

    public static PaymentDetails Create(decimal amount) => new(type: GetProviderType(), amount: amount, isSuccessful: false);

    private static string GetProviderType() => new Faker().PickRandom(Types);

    public void Pay() => IsSuccessful = true;
}
