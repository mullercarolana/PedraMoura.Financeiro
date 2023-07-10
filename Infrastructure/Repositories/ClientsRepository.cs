using ApplicationCore.Entities.Clients;
using ApplicationCore.Seedwork.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class ClientsRepository : IClientsRepository
    {
        private readonly FinancialDbContext _context;
        public ClientsRepository(FinancialDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(Client client)
        {
            _context.Set<Client>().Add(client);
        }

        public async Task<Result<Client>> GetClientAsync(int id)
        {
            try
            {
                var client = await _context
                    .Clients
                    .Include(c => c.Addresses)
                    .Include(c => c.IdentityCards)
                    .FirstOrDefaultAsync(c => c.Id == id);

                return client is null
                    ? Error.New("ClientCannotBeFound", "Client cannot be found.")
                    : client;
            }
            catch (Exception ex)
            {
                return Error.New("ErrorGetClient", $"Error to get client. Error: {ex.Message}");
            }
        }
    }
}
