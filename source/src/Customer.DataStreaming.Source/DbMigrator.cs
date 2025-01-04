using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source;

public class DbMigrator<TDbContext>(TDbContext dbContext)
    where TDbContext : DbContext
{
    public async Task MigrateAsync(CancellationToken cancellationToken) 
        => await dbContext.Database.MigrateAsync(cancellationToken);
}