using ApplicationCore.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class ProductOrderMap : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.ToTable("ProductOrder");
            builder.HasKey(p => p.Id);
            builder
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey("ProductId")
                .IsRequired();
            builder.Property(p => p.Amount).HasColumnType("int");
            builder.Property(p => p.TotalValue).HasColumnType("decimal(18,4)");
        }
    }
}
