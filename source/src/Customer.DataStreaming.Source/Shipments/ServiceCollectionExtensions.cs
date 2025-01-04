using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Shipments;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShipment(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Shipments");

        services.AddDbContext<ShipmentContext>(options => options
            .UseNpgsql(connectionString, n => { }).UseSnakeCaseNamingConvention());
        services.AddHostedService<ShipmentSourceService>();
        services.AddScoped<DbMigrator<ShipmentContext>>();

        services.Configure<ShipmentSourceOptions>(configuration.GetRequiredSection("ShipmentSource"));

        return services;
    }
}