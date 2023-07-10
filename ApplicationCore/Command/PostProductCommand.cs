using ApplicationCore.Entities.Products;
using ApplicationCore.Seedwork.Exceptions;
using MediatR;

namespace ApplicationCore.Command
{
    public sealed class PostProductCommand : IRequest<Result<Product>>
    {
        private PostProductCommand(string name, decimal price, string massMeasure, decimal value)
        {
            Name = name;
            Price = price;
            MassMeasure = massMeasure;
            Value = value;
        }

        public string Name { get; }
        public decimal Price { get; }
        public string MassMeasure { get; }
        public decimal Value { get; }

        public static PostProductCommand Create(string name, decimal price, string massMeasure, decimal value)
        {
            return new PostProductCommand(name, price, massMeasure, value);
        }
    }
}
