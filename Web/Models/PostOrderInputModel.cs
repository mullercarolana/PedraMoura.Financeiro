namespace Web.Models
{
    public sealed class PostOrderInputModel
    {
        public int ClientId { get; set; }
        public AddressInputModel Address { get; set; }
        public ProductOrderInputModel[] Products { get; set; }
        public string TypeOfPayment { get; set; }
    }

    public sealed class ProductOrderInputModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }

    public sealed class AddressInputModel
    {
        public bool FavoriteAddress { get; set; }
        public int? AddressId { get; set; }
    }
}
