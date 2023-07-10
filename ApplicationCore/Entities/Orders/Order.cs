using ApplicationCore.Entities.Clients;
using ApplicationCore.Seedwork.Exceptions;

namespace ApplicationCore.Entities.Orders
{
    public sealed class Order
    {
        private Order() { }
        private Order(int id, Client client, int addressId, EStatusOrder status, decimal totalAmountPayable, DateTime dateOrder, List<ProductOrder> productOrders)
        {
            Id = id;
            Client = client;
            AddressId = addressId;
            Status = status;
            TotalAmountPayable = totalAmountPayable;
            DateOrder = dateOrder;
            _productOrders = productOrders;
        }

        private readonly IList<ProductOrder> _productOrders;
        public int Id { get; private set; }
        public Client Client { get; private set; }
        public int AddressId { get; private set; }
        public EStatusOrder Status { get; set; }
        public PaymentOrder PaymentOrder { get; private set; }
        public decimal TotalAmountPayable { get; private set; }
        public DateTime DateOrder { get; private set; }
        public IEnumerable<ProductOrder> ProductOrders => _productOrders ?? new List<ProductOrder>();

        public static Order CreateInProgress(Client client, int addressId, List<ProductOrder> productOrders, int id = 0)
        {
            var total = productOrders.Sum(p => p.TotalValue);
            return new Order(id, client, addressId, EStatusOrder.InProgress, total, DateTime.MinValue, productOrders);
        }

        public static Result<ETypePayment> GetTypePayment(string type)
        {
            if (!type.Equals(ETypePayment.Pix.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return Error.New("TypeIsInvalid", "Payment type is invalid.");
            }
            return ETypePayment.Pix;
        }

        public void SetInProgressPayment(PaymentOrder paymentOrder)
        {
            Status = EStatusOrder.InProgress;
            PaymentOrder = paymentOrder;
        }

        public void SetSuccessfulPayment(PaymentOrder paymentOrder)
        {
            Status = EStatusOrder.Done;
            PaymentOrder = paymentOrder;
            DateOrder = DateTime.UtcNow;
        }

        public void SetCanceledPayment()
        {
            Status = EStatusOrder.Canceled;
        }
    }

    public enum EStatusOrder
    {
        InProgress = 0,
        Done = 1,
        Canceled = 2
    }

    public enum ETypePayment
    {
        Pix = 0
    }
}
