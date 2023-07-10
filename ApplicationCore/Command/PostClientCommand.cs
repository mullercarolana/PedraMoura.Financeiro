using ApplicationCore.Entities.Clients;
using ApplicationCore.Seedwork.Exceptions;
using MediatR;

namespace ApplicationCore.Command
{
    public sealed class PostClientCommand : IRequest<Result<Client>>
    {
        private PostClientCommand(
            string name,
            string phone,
            string email,
            string street,
            string number,
            string complement,
            string city,
            string state,
            string country,
            string zipCode,
            string type,
            string value,
            DateTime expiration
        )
        {
            Name = name;
            Phone = phone;
            Email = email;
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Type = type;
            Value = value;
            Expiration = expiration;
        }

        public string Name { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Street { get; }
        public string Number { get; }
        public string Complement { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }
        public string Type { get; }
        public string Value { get; }
        public DateTime Expiration { get; }

        public static PostClientCommand Create(
            string name,
            string phone,
            string email,
            string street,
            string number,
            string complement,
            string city,
            string state,
            string country,
            string zipCode,
            string type,
            string value,
            DateTime expiration
        )
        {
            return new PostClientCommand(name, phone, email, street, number, complement, city, state, country, zipCode, type, value, expiration);
        }
    }
}
