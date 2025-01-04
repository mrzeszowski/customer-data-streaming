using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Customers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomers(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Customers");

        services.AddDbContext<CustomerContext>(options => options
            .UseNpgsql(connectionString, n => { }).UseSnakeCaseNamingConvention());
        services.AddHostedService<CustomerSourceService>();
        services.AddScoped<DbMigrator<CustomerContext>>();

        services.Configure<CustomerSourceOptions>(configuration.GetRequiredSection("CustomerSource"));

        return services;
    }
}