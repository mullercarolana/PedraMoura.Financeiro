using ApplicationCore.Seedwork.Exceptions;

namespace ApplicationCore.Entities.Products
{
    public interface IProductsRepository
    {
        Task SaveChangesAsync();
        void Add(Product product);
        Task<Result<Product>> GetProductsAsync(int id);
    }
}
