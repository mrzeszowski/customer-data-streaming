using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Transactions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTransaction(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Transactions");

        services.AddDbContext<TransactionContext>(options => options
            .UseNpgsql(connectionString, n => { }).UseSnakeCaseNamingConvention());
        services.AddHostedService<TransactionSourceService>();
        services.AddScoped<DbMigrator<TransactionContext>>();

        services.Configure<TransactionSourceOptions>(configuration.GetRequiredSection("TransactionSource"));

        return services;
    }
}