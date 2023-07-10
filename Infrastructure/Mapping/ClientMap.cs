using ApplicationCore.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public sealed class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("varchar(250)");
            builder.Property(c => c.Phone).HasColumnType("varchar(13)");
            builder.Property(c => c.Phone).HasColumnType("varchar(250)");
            
            builder
                .HasMany(c => c.Addresses)
                .WithOne()
                .HasForeignKey("ClientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_addresses");

            builder
                .HasMany(c => c.IdentityCards)
                .WithOne()
                .HasForeignKey("ClientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_identityCards");
        }
    }
}
