using Customer.DataStreaming.Source.Customers;
using Customer.DataStreaming.Source.Shipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Customer.DataStreaming.Source.Transactions;

public class TransactionSourceService(IServiceProvider serviceProvider,
    ILogger<TransactionSourceService> logger,
    IOptions<TransactionSourceOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Yield();

        logger.LogInformation("TransactionSourceService Delay in Seconds set to: {0}", options.Value.ServiceDelay);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.WhenAll(Enumerable.Range(0, options.Value.CreateCount).Select(_ => CreateAsync(cancellationToken)));
                
                foreach (var _ in Enumerable.Range(0, options.Value.PayCount)) 
                    await PayAsync(cancellationToken);
                
                logger.LogInformation($"{nameof(TransactionSourceService)} successfully finished the work."); 

                await Task.Delay(TimeSpan.FromSeconds(options.Value.ServiceDelay + new Random().Next(0, 2)), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(TransactionSourceService)} encountered an error.");
            }
        }
    }

    private async Task CreateAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var customersContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
        var transactionContext = scope.ServiceProvider.GetRequiredService<TransactionContext>();

        var customer = await customersContext.Set<Customers.Customer>()
            .AsNoTracking()
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefaultAsync(cancellationToken);

        if (customer is null)
            return;

        var unlucky = new Random().Next(1, 100) == 7;
        
        var products = Products.ProductCatalog.Products.OrderBy(x => Guid.NewGuid())
            .Take(unlucky ? new Random().Next(10, 15) : new Random().Next(1, 3))
            .Select(x => new TransactionProduct(x.Id, x.Name, x.Price, unlucky ? new Random().Next(30, 40) : 1))
            .ToArray();
        
        var shipmentDetails = ShipmentDetails.Create(customer.Address);
        var payment = PaymentDetails.Create(products.Sum(x => x.Price * x.Quantity));

        var transaction = Transaction.Create(customer.Id, products, shipmentDetails, payment);
        
        if (unlucky) 
            logger.LogWarning("Unlucky transaction {0}", transaction.Id);
        
        await transactionContext.AddAsync(transaction, cancellationToken);
        await transactionContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task PayAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var transactionContext = scope.ServiceProvider.GetRequiredService<TransactionContext>();
        

        var transaction = await transactionContext.Set<Transaction>()
            .Where(x => x.Payment.IsSuccessful == false)
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefaultAsync(cancellationToken);

        if (transaction is null)
            return;
        
        transaction.Pay();
        
        await transactionContext.SaveChangesAsync(cancellationToken);
        
        var shipmentContext = scope.ServiceProvider.GetRequiredService<ShipmentContext>();
        var shipment = Shipment.Create(transaction.Id);
        
        await shipmentContext.AddAsync(shipment, cancellationToken);
        await shipmentContext.SaveChangesAsync(cancellationToken);
    }
}