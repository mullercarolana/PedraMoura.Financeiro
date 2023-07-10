using ApplicationCore.Seedwork;

namespace ApplicationCore.ValuesObjects
{
    public sealed class Address : ValueObject
    {
        private Address() { }
        private Address(int id, string street, string number, string complement, string city, string state, string country, string zipcode)
        {
            Id = id;
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        public int Id { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public bool IsFavorite { get; private set; }

        public static Address Create(string street, string number, string complement, string city, string state, string country, string zipcode, int id = 0)
        {
            return new Address(id, street, number, complement, city, state, country, zipcode);
        }

        public Address SetAsFavorite()
        {
            IsFavorite = true;
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Street;
            yield return Number;
            yield return Complement;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
            yield return IsFavorite;
        }
    }
}
