using Microsoft.EntityFrameworkCore;

namespace Customer.DataStreaming.Source.Shipments
{
    public class ShipmentContext(DbContextOptions<ShipmentContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("shipments");
            
            // Apply configurations
            modelBuilder.ApplyConfiguration(new EfShipmentConfiguration());
        }
    }
}