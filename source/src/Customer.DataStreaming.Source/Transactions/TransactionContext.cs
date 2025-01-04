using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Transactions
{
    public class TransactionContext(DbContextOptions<TransactionContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("transactions");
            
            // Apply configurations
            modelBuilder.ApplyConfiguration(new EfTransactionConfiguration());
        }
    }
}