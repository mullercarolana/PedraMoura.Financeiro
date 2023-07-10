using ApplicationCore.Entities.Clients;
using ApplicationCore.Entities.Orders;
using ApplicationCore.Entities.Products;
using ApplicationCore.ValuesObjects;
using Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public sealed class FinancialDbContext : DbContext
    {
        public FinancialDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<IdentityCard> IdentityCards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }
        public DbSet<Pix> Pix { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new IdentityCardMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductOrderMap());
            modelBuilder.ApplyConfiguration(new PaymentOrderMap());
            modelBuilder.ApplyConfiguration(new PixMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
