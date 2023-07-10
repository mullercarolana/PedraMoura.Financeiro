using ApplicationCore.Seedwork.Exceptions;

namespace ApplicationCore.Entities.Products
{
    public sealed class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public EMassMeasureType MassMeasure { get; private set; }
        public decimal Value { get; private set; }

        private Product() { }

        private Product(int id, string name, decimal price, EMassMeasureType massMeasure, decimal value)
        {
            Id = id;
            Name = name;
            Price = price;
            MassMeasure = massMeasure;
            Value = value;
        }

        public static Result<Product> Create(string name, decimal price, string massMeasure, decimal value, int id = 0)
        {
            if (price < 0)
            {
                return Error.New("PriceCannotBeZero", "Price cannot be zero.");
            }

            if (value < 0)
            {
                return Error.New("MassMeasureValueCannotBeZero", "Mass measure value cannot be zero.");
            }

            return GetMassMeasure(massMeasure) is var eMassMeasureType && eMassMeasureType.IsError
                ? eMassMeasureType.Error
                : new Product(id, name, price, eMassMeasureType.Success, value);
        }

        public static Result<EMassMeasureType> GetMassMeasure(string massMeasure)
        {
            var result = !massMeasure.Equals(EMassMeasureType.Kilogram.ToString(), StringComparison.CurrentCultureIgnoreCase)
                && !massMeasure.Equals(EMassMeasureType.Gram.ToString(), StringComparison.CurrentCultureIgnoreCase)
                && !massMeasure.Equals(EMassMeasureType.Milligram.ToString(), StringComparison.CurrentCultureIgnoreCase);

            if (result is true)
            {
                return Error.New("MassMeasureTypeInvalid", "Mass measure type is invalid.");
            }

            return massMeasure.ToLower() switch
            {
                "kilogram" => (Result<EMassMeasureType>)EMassMeasureType.Kilogram,
                "gram" => (Result<EMassMeasureType>)EMassMeasureType.Gram,
                "milligram" => (Result<EMassMeasureType>)EMassMeasureType.Milligram,
                _ => (Result<EMassMeasureType>)EMassMeasureType.Gram,
            };
        }
    }

    public enum EMassMeasureType
    {
        Kilogram = 0,
        Gram = 1,
        Milligram = 2
    }
}
