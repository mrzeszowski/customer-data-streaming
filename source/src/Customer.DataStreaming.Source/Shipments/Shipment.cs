using System.Text.Json.Serialization;

namespace Customer.DataStreaming.Source.Shipments;

public class Shipment
{
    public Guid Id { get; private set; }
    public Guid TransactionId { get; private set; }

    private List<ShipmentStage> _stages = new();
    public IReadOnlyList<ShipmentStage> Stages => _stages;
    
    public bool IsCompleted { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public long Version { get; private set; }

    private Shipment() { }
    
    public Shipment(Guid id,
        Guid transactionId,
        IReadOnlyList<ShipmentStage> stages,
        bool isCompleted,
        DateTime createdAt,
        DateTime? updatedAt = null,
        long version = 1)
    {
        Id = id;
        TransactionId = transactionId;
        _stages = stages.ToList();
        IsCompleted = isCompleted;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Version = version;
    }

    public static string[] Types = new[]
    {
        "Created", "Processed", "Shipped", "TransitStarted", "Delivered",
    };
    
    public static Shipment Create(Guid transactionId)
    {
        return new Shipment(Guid.NewGuid(),
            transactionId,
            new[] { new ShipmentStage(Types[0], DateTime.UtcNow) },
            false,
            DateTime.UtcNow);
    }

    public void NextStage()
    {
        switch (Stages[^1].Type)
        {
            case "Created":
                _stages.Add(new ShipmentStage(Types[1], DateTime.UtcNow));
                break;
            case "Processed":
                _stages.Add(new ShipmentStage(Types[2], DateTime.UtcNow));
                break;
            case "Shipped":
                _stages.Add(new ShipmentStage(Types[3], DateTime.UtcNow));
                break;
            case "TransitStarted":
                _stages.Add(new ShipmentStage(Types[4], DateTime.UtcNow));
                IsCompleted = true;
                break;
            case "Delivered":
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        UpdatedAt = DateTime.UtcNow;
        Version++;
    }
}

public class ShipmentStage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string? Note { get; private set; }

    public ShipmentStage(string type, DateTime timestamp, string? note = null)
    {
        Id = Guid.NewGuid();
        Type = type;
        Timestamp = timestamp;
        Note = note;
    }
}