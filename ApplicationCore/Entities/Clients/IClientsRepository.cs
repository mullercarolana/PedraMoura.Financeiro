using ApplicationCore.Seedwork.Exceptions;

namespace ApplicationCore.Entities.Clients
{
    public interface IClientsRepository
    {
        Task SaveChangesAsync();
        void Add(Client client);
        Task<Result<Client>> GetClientAsync(int id);
    }
}
