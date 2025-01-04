using Customer.DataStreaming.Source.Customers;
using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Products;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProducts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Products");

        services.AddDbContext<ProductContext>(options => options
            .UseNpgsql(connectionString, n => { }).UseSnakeCaseNamingConvention());
        services.AddHostedService<ProductSourceService>();
        services.AddScoped<DbMigrator<ProductContext>>();

        services.Configure<ProductSourceOptions>(configuration.GetRequiredSection("ProductSource"));

        return services;
    }
}