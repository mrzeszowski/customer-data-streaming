using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Products
{
    public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("products");
            
            // Apply configurations
            modelBuilder.ApplyConfiguration(new EfProductConfiguration());
        }
    }
}