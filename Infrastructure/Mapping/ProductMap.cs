using ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar(300)");
            builder.Property(p => p.Price).HasColumnType("decimal(18,4)");
            builder.Property(p => p.MassMeasure).HasConversion(
                v => v.ToString(),
                v => (EMassMeasureType)Enum.Parse(typeof(EMassMeasureType), v)).HasColumnType("varchar(10)");
            builder.Property(p => p.Value).HasColumnType("decimal(18,4)");
        }
    }
}
