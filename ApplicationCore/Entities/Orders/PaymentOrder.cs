namespace ApplicationCore.Entities.Orders
{
    public abstract class PaymentOrder
    {
        protected PaymentOrder() { }
        protected PaymentOrder(int id, EStatusPaymentOrder status, DateTime dateStatusApproved, string canceledReason, DateTime dateStatusCanceled)
        {
            Id = id;
            Status = status;
            DateStatusApproved = dateStatusApproved;
            CanceledReason = canceledReason;
            DateStatusCanceled = dateStatusCanceled;
        }

        public int Id { get; set; }
        public EStatusPaymentOrder Status { get; set; }
        public DateTime DateStatusApproved { get; set; }
        public string CanceledReason { get; set; }
        public DateTime DateStatusCanceled { get; set; }
    }

    public enum EStatusPaymentOrder
    {
        Processing = 0,
        Approved = 1,
        Canceled = 2
    }
}
