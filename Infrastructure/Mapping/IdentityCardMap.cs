using ApplicationCore.ValuesObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class IdentityCardMap : IEntityTypeConfiguration<IdentityCard>
    {
        public void Configure(EntityTypeBuilder<IdentityCard> builder)
        {
            builder.ToTable("IdentityCard");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Type).HasConversion(
                v => v.ToString(),
                v => (EIdentityType)Enum.Parse(typeof(EIdentityType), v)).HasColumnType("varchar(10)");
            builder.Property(i => i.Value).HasColumnType("varchar(14)").IsRequired();
            builder.Property(i => i.Expiration).HasColumnType("date");
            builder.Property(i => i.IsFavorite).HasColumnType("bit");
        }
    }
}
