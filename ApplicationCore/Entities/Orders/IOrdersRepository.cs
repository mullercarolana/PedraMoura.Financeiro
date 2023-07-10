using ApplicationCore.Seedwork.Exceptions;

namespace ApplicationCore.Entities.Orders
{
    public interface IOrdersRepository
    {
        Task SaveChangesAsync();
        void Add(Order order);
        Task<Result<Pix>> GetPixAsync(string txid);
    }
}
