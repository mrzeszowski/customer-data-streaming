namespace Customer.DataStreaming.Source.Shipments;

public class ShipmentSourceOptions
{
    public int ServiceDelay { get; set; } = 5;
    public int ProcessCount { get; set; } = 10;
}