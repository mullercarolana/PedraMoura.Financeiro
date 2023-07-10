using ApplicationCore.ValuesObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Street).HasColumnType("varchar(35)");
            builder.Property(a => a.Number).HasColumnType("varchar(6)");
            builder.Property(a => a.Complement).HasColumnType("varchar(15)");
            builder.Property(a => a.City).HasColumnType("varchar(35)");
            builder.Property(a => a.State).HasColumnType("varchar(2)");
            builder.Property(a => a.Country).HasColumnType("varchar(30)");
            builder.Property(a => a.ZipCode).HasColumnType("varchar(10)");
        }
    }
}
