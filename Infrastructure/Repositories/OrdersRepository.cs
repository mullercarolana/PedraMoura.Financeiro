using ApplicationCore.Entities.Orders;
using ApplicationCore.Seedwork.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class OrdersRepository : IOrdersRepository
    {
        private readonly FinancialDbContext _context;
        public OrdersRepository(FinancialDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(Order order)
        {
            _context.Set<Order>().Add(order);
        }

        public async Task<Result<Pix>> GetPixAsync(string txid)
        {
            try
            {
                var order = await _context
                    .PaymentOrders
                    .Cast<Pix>()
                    .Include(p => p.PixPayment)
                    .ThenInclude(p => p.Calendario)
                    .Include(p => p.PixPayment)
                    .ThenInclude(p => p.Loc)
                    .Include(p => p.PixPayment)
                    .ThenInclude(p => p.Devedor)
                    .Include(p => p.PixPayment)
                    .ThenInclude(p => p.Valor)
                    .FirstOrDefaultAsync(p => p.PixPayment.Txid == txid);

                return order is null
                    ? Error.New("PaymentOrderCannotBeFound", "Payment order cannot be found.")
                    : order;
            }
            catch (Exception ex)
            {
                return Error.New("ErrorGetPaymentOrder", $"Error to get payment order. Error: {ex.Message}");
            }
        }
    }
}
