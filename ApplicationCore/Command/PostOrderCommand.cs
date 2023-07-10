using ApplicationCore.Entities.Orders;
using ApplicationCore.Seedwork.Exceptions;
using MediatR;

namespace ApplicationCore.Command
{
    public sealed class PostOrderCommand : IRequest<Result<Order>>
    {
        private PostOrderCommand(int clientId, AddressCommand address, List<ProductCommand> products, string typeOfPayment)
        {
            ClientId = clientId;
            Address = address;
            Products = products;
            TypeOfPayment = typeOfPayment;
        }

        public int ClientId { get; }
        public AddressCommand Address { get; }
        public List<ProductCommand> Products { get; }
        public string TypeOfPayment { get; }

        public static PostOrderCommand Create(int clientId, AddressCommand address, List<ProductCommand> products, string typeOfPayment)
        {
            return new PostOrderCommand(clientId, address, products, typeOfPayment);
        }
    }

    public sealed class ProductCommand
    {
        private ProductCommand(int productId, int amount)
        {
            ProductId = productId;
            Amount = amount;
        }

        public int ProductId { get; }
        public int Amount { get; }

        public static ProductCommand Create(int productId, int amount)
        {
            return new ProductCommand(productId, amount);
        }
    }

    public sealed class AddressCommand
    {
        private AddressCommand(bool favoriteAddress, int? addressId)
        {
            FavoriteAddress = favoriteAddress;
            AddressId = addressId;
        }

        public bool FavoriteAddress { get; }
        public int? AddressId { get; }

        public static AddressCommand Create(bool favoriteAddress, int? addressId)
        {
            return new AddressCommand(favoriteAddress, addressId);
        }
    }
}
