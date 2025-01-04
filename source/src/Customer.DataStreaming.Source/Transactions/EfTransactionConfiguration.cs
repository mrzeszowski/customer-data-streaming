using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.DataStreaming.Source.Transactions
{
    public class EfTransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transaction");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.Version).IsConcurrencyToken();
            
            builder.OwnsOne(x => x.Payment, x =>
            {
                x.ToJson();
                x.Property(p => p.Type).IsRequired().HasMaxLength(200);
            });
            builder.OwnsOne(x => x.Shipment, x =>
            {
                x.ToJson();
                x.Property(s => s.Address).IsRequired().HasMaxLength(400);
                x.Property(s => s.Provider).IsRequired().HasMaxLength(50);
            });
            builder.OwnsMany(x => x.Products, x =>
            {
                x.ToJson();
                x.Property(p => p.Name).HasMaxLength(100);
            });
        }
    }
}
