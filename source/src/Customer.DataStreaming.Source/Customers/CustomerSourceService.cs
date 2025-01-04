using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Customer.DataStreaming.Source.Customers;

public class CustomerSourceService(IServiceProvider serviceProvider,
    ILogger<CustomerSourceService> logger,
    IOptions<CustomerSourceOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Yield();

        logger.LogInformation("CustomerSourceService Delay in Seconds set to: {0}", options.Value.ServiceDelay);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.WhenAll(Enumerable.Range(0, options.Value.CreateCount).Select(_ => CreateCustomerAsync(cancellationToken)));

                foreach (var _ in Enumerable.Range(0, options.Value.UpdateCount)) 
                    await UpdateCustomersAsync(cancellationToken);
                 
                logger.LogInformation($"{nameof(CustomerSourceService)} successfully finished the work."); 

                await Task.Delay(TimeSpan.FromSeconds(options.Value.ServiceDelay + new Random().Next(0, 2)), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(CustomerSourceService)} encountered an error.");
            }
        }
    }

    private async Task CreateCustomerAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();

        var customer = new CustomerFaker().Generate();
        await dbContext.Set<Customer>().AddAsync(customer, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task UpdateCustomersAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();

        var customer = await dbContext.Set<Customer>().OrderBy(x => Guid.NewGuid()).FirstAsync(cancellationToken);
        customer.Update();

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}