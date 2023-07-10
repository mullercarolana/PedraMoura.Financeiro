using ApplicationCore.Entities.Products;

namespace ApplicationCore.Entities.Orders
{
    public sealed class ProductOrder
    {
        private ProductOrder() { }
        private ProductOrder(int id, Product product, int amount)
        {
            Id = id;
            Product = product;
            Amount = amount;
        }

        public int Id { get; private set; }
        public Product Product { get; private set; }
        public int Amount { get; private set; }
        public decimal TotalValue { get; private set; }

        public static ProductOrder Create(Product product, int amount, int id = 0)
        {
            var productOrder = new ProductOrder(id, product, amount);
            var totalValue = productOrder.Calculate(product);
            return productOrder.SetTotalValue(totalValue);
        }

        public decimal Calculate(Product product)
        {
            return product.Price * product.Value + Amount;
        }

        public ProductOrder SetTotalValue(decimal totalValue)
        {
            TotalValue = totalValue;
            return this;
        }
    }
}
