using ApplicationCore.Seedwork.Exceptions;
using ApplicationCore.ValuesObjects;

namespace ApplicationCore.Entities.Clients
{
    public sealed class Client
    {
        private Client() { }
        private Client(int id, string name, string phone, string email, List<Address> addresses, List<IdentityCard> identityCards)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            _addresses = addresses;
            _identityCards = identityCards;
        }

        private readonly IList<Address> _addresses;
        private readonly IList<IdentityCard> _identityCards;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<Address> Addresses => _addresses ?? new List<Address>();
        public IEnumerable<IdentityCard> IdentityCards => _identityCards ?? new List<IdentityCard>();

        public static Result<Client> Create(string name, string phone, string email, Address address, IdentityCard identityCard, int id = 0)
        {
            if (address is null)
            {
                return Error.New("AddressIsNull", "Address cannot to be null.");
            }

            if (identityCard is null)
            {
                return Error.New("IdentityCardIsNull", "Identity card cannot to be null.");
            }

            return new Client(id, name, phone, email, new List<Address>() { address.SetAsFavorite() }, new List<IdentityCard>() { identityCard.SetAsFavorite() });
        }

        public Result<Address> GetFavoriteAdress()
        {
            return _addresses.FirstOrDefault(a => a.IsFavorite) is var address && address is null
                ? Error.New("AddressNotFound", "Favorite address cannot to find.")
                : address;
        }

        public Result<Address> GetAddress(int id)
        {
            return _addresses.FirstOrDefault(a => a.Id == id) is var address && address is null
                ? Error.New("AddressNotFound", "Address cannot to find.")
                : address;
        }

        public Result<IdentityCard> GetFavoriteIdentityCard()
        {
            return _identityCards.FirstOrDefault(i => i.IsFavorite) is var identityCard && identityCard is null
                ? Error.New("IdentityCardNotFound", "Favorite identity card cannot to find.")
                : identityCard;
        }

        public Result<IdentityCard> GetIdentityCard(int id)
        {
            return _identityCards.FirstOrDefault(i => i.Id == id) is var identityCard && identityCard is null
                ? Error.New("IdentityCardNotFound", "Identity card cannot to find.")
                : identityCard;
        }
    }
}
