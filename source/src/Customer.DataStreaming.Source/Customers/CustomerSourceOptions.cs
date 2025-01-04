namespace Customer.DataStreaming.Source.Customers;

public class CustomerSourceOptions
{
    public int ServiceDelay { get; set; } = 5;
    public int CreateCount { get; set; } = 1;
    public int UpdateCount { get; set; } = 5;
}