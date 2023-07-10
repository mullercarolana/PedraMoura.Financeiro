using ApplicationCore.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);

            builder
                .HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .IsRequired();

            builder
                .HasOne(o => o.PaymentOrder)
                .WithMany()
                .HasForeignKey("PaymentOrderId")
                .IsRequired(false);

            builder
                .HasMany(o => o.ProductOrders)
                .WithOne()
                .HasForeignKey("OrderId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_productOrders");

            builder.Property(o => o.AddressId).HasColumnType("int");
            builder.Property(o => o.Status).HasConversion(
                v => v.ToString(),
                v => (EStatusOrder)Enum.Parse(typeof(EStatusOrder), v)).HasColumnType("varchar(10)");
            builder.Property(o => o.TotalAmountPayable).HasColumnType("decimal(18,4)");
            builder.Property(o => o.DateOrder).HasColumnType("datetime2");
        }
    }
}
