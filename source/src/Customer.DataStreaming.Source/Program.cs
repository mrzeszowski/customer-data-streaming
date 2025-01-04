using Customer.DataStreaming.Source;
using Customer.DataStreaming.Source.Customers;
using Customer.DataStreaming.Source.Products;
using Customer.DataStreaming.Source.Shipments;
using Customer.DataStreaming.Source.Transactions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomers(builder.Configuration)
    .AddProducts(builder.Configuration)
    .AddTransaction(builder.Configuration)
    .AddShipment(builder.Configuration);

var app = builder.Build();

await using (var asyncServiceScope = app.Services.CreateAsyncScope())
{
    await asyncServiceScope.ServiceProvider.GetRequiredService<DbMigrator<CustomerContext>>().MigrateAsync(CancellationToken.None);
    await asyncServiceScope.ServiceProvider.GetRequiredService<DbMigrator<ProductContext>>().MigrateAsync(CancellationToken.None);
    await asyncServiceScope.ServiceProvider.GetRequiredService<DbMigrator<ShipmentContext>>().MigrateAsync(CancellationToken.None);
    await asyncServiceScope.ServiceProvider.GetRequiredService<DbMigrator<TransactionContext>>().MigrateAsync(CancellationToken.None);
}

app.MapGet("/", () => "I am alive!");

app.Run();