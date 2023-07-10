using ApplicationCore.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class PaymentOrderMap : IEntityTypeConfiguration<PaymentOrder>
    {
        public void Configure(EntityTypeBuilder<PaymentOrder> builder)
        {
            builder.ToTable("PaymentOrder");
            builder.HasKey(o => o.Id);
            builder.HasDiscriminator<string>("Type").HasValue<Pix>(nameof(Pix));
            builder.Property(o => o.Status).HasConversion(
                v => v.ToString(),
                v => (EStatusPaymentOrder)Enum.Parse(typeof(EStatusPaymentOrder), v)).HasColumnType("varchar(10)");
            builder.Property(o => o.DateStatusApproved).HasColumnType("datetime2");
            builder.Property(o => o.CanceledReason).HasColumnType("varchar(max)");
            builder.Property(o => o.DateStatusCanceled).HasColumnType("datetime2");
        }
    }
}
