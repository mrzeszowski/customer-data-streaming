namespace Customer.DataStreaming.Source.Transactions;

public class TransactionSourceOptions
{
    public int ServiceDelay { get; set; } = 5;
    public int CreateCount { get; set; } = 10;
    public int PayCount { get; set; } = 5;
}