using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Customer.DataStreaming.Source.Shipments;

public class ShipmentSourceService(IServiceProvider serviceProvider,
    ILogger<ShipmentSourceService> logger,
    IOptions<ShipmentSourceOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Yield();

        logger.LogInformation("ShipmentSourceService Delay in Seconds set to: {0}", options.Value.ServiceDelay);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                foreach (var _ in Enumerable.Range(0, options.Value.ProcessCount)) 
                    await ProcessAsync(cancellationToken);
                
                logger.LogInformation($"{nameof(ShipmentSourceService)} successfully finished the work."); 

                await Task.Delay(TimeSpan.FromSeconds(options.Value.ServiceDelay + new Random().Next(0, 2)), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(ShipmentSourceService)} encountered an error.");
            }
        }
    }
    
    private async Task ProcessAsync(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShipmentContext>();

        var shipment = await dbContext.Set<Shipment>()
            .Where(x => x.IsCompleted == false)
            .OrderBy(x => Guid.NewGuid())
            .FirstOrDefaultAsync(cancellationToken);

        if (shipment is null)
            return;
        
        shipment.NextStage();

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}