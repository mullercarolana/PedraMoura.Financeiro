using ApplicationCore.Command;
using ApplicationCore.Entities.Clients;
using ApplicationCore.Seedwork.Exceptions;
using ApplicationCore.ValuesObjects;
using MediatR;

namespace ApplicationCore.Handler
{
    public sealed class PostClientHandler : IRequestHandler<PostClientCommand, Result<Client>>
    {
        private readonly IClientsRepository _clientsRepository;
        public PostClientHandler(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task<Result<Client>> Handle(PostClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var address = Address.Create(request.Street, request.Number, request.Complement, request.City, request.State, request.Country, request.ZipCode);

                if (IdentityCard.Create(request.Type, request.Value, request.Expiration) is var identityCard && identityCard.IsError)
                {
                    return identityCard.Error;
                }

                if (Client.Create(request.Name, request.Phone, request.Email, address, identityCard.Success) is var client && client.IsError)
                {
                    return client.Error;
                }

                _clientsRepository.Add(client.Success);
                await _clientsRepository.SaveChangesAsync();
                return client.Success;
            }
            catch (Exception ex)
            {
                return Error.New("ErrorPostClient", $"Error to post the client. Error: {ex.Message}");
            }
        }
    }
}
