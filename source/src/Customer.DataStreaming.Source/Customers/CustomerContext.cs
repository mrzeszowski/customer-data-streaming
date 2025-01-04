using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Customers
{
    public class CustomerContext(DbContextOptions<CustomerContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("customers");
            
            // Apply configurations
            modelBuilder.ApplyConfiguration(new EfCustomerConfiguration());
        }
    }
}