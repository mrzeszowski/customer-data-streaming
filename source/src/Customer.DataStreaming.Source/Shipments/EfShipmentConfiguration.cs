using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.DataStreaming.Source.Shipments
{
    public class EfShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("shipment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.TransactionId).IsRequired();
            builder.Property(x => x.IsCompleted).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.Version).IsConcurrencyToken();
            
            builder.OwnsMany(x => x.Stages, x =>
            {
                x.ToJson();
                x.Property(s => s.Type).IsRequired().HasMaxLength(50);
                x.Property(s => s.Note).HasMaxLength(400);
            });
        }
    }
}
