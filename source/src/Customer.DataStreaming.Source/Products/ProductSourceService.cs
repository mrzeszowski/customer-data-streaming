using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Customer.DataStreaming.Source.Products;

public class ProductSourceService(IServiceProvider serviceProvider,
    ILogger<ProductSourceService> logger,
    IOptions<ProductSourceOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Yield();

        logger.LogInformation("ProductSourceService Delay in Seconds set to: {0}", options.Value.ServiceDelay);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.WhenAll(ProductCatalog.Products.Select(x => UpsertProductAsync(x, cancellationToken)));
                 
                logger.LogInformation($"{nameof(ProductSourceService)} successfully finished the work."); 

                await Task.Delay(TimeSpan.FromSeconds(options.Value.ServiceDelay), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(ProductSourceService)} encountered an error.");
            }
        }
    }

    private async Task UpsertProductAsync(Product product, CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>();

        var entity = await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == product.Id, cancellationToken);

        if (entity is null)
            await dbContext.Set<Product>().AddAsync(product, cancellationToken);
        else
            entity.Update(product);
        

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}