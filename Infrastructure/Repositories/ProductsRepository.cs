using ApplicationCore.Entities.Products;
using ApplicationCore.Seedwork.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class ProductsRepository : IProductsRepository
    {
        private readonly FinancialDbContext _context;
        public ProductsRepository(FinancialDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(Product product)
        {
            _context.Set<Product>().Add(product);
        }

        public async Task<Result<Product>> GetProductsAsync(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                return product is null
                    ? Error.New("ProductCannotBeFound", "Product cannot be found.")
                    : product;
            }
            catch (Exception ex)
            {
                return Error.New("ErrorGetProduct", $"Error to get product. Error: {ex.Message}");
            }
        }
    }
}
