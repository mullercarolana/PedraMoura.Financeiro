using ApplicationCore.Shared.Pix;

namespace ApplicationCore.Entities.Orders
{
    public sealed class Pix : PaymentOrder
    {
        private Pix() : base() { }
        private Pix(
            int id,
            EStatusPaymentOrder status,
            DateTime dateStatusApproved,
            string canceledReason,
            DateTime dateStatusCanceled,
            PixPayment pixPayment
        ) : base(id, status, dateStatusApproved, canceledReason, dateStatusCanceled)
        {
            PixPayment = pixPayment;
        }

        public PixPayment PixPayment { get; private set; }

        public static Pix CreateInProcessing(int id = 0)
        {
            return new Pix(id, EStatusPaymentOrder.Processing, DateTime.MinValue, null, DateTime.MinValue, null);
        }

        public void SetApproved(PixPayment pixPayment)
        {
            Status = EStatusPaymentOrder.Approved;
            DateStatusApproved = DateTime.UtcNow;
            PixPayment = pixPayment;
        }

        public void SetCanceled(string reason)
        {
            DateStatusCanceled = DateTime.UtcNow;
            CanceledReason = reason;
        }
    }
}
