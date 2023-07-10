using ApplicationCore.Seedwork;
using ApplicationCore.Seedwork.Exceptions;

namespace ApplicationCore.ValuesObjects
{
    public sealed class IdentityCard : ValueObject
    {
        private IdentityCard() { }
        private IdentityCard(int id, EIdentityType type, string value, DateTime expiration)
        {
            Id = id;
            Type = type;
            Value = value;
            Expiration = expiration;
        }

        public int Id { get; private set; }
        public EIdentityType Type { get; private set; }
        public string Value { get; private set; }
        public DateTime Expiration { get; private set; }
        public bool IsFavorite { get; private set; }

        public static Result<IdentityCard> Create(string type, string value, DateTime expiration, int id = 0)
        {
            return GetIdentityType(type) is var identityType && identityType.IsError
                ? identityType.Error
                : new IdentityCard(id, identityType.Success, value, expiration);
        }

        public static Result<EIdentityType> GetIdentityType(string type)
        {
            if (!type.Equals(EIdentityType.CPF.ToString(), StringComparison.CurrentCultureIgnoreCase) && !type.Equals(EIdentityType.RG.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return Error.New("TypeIsInvalid", "Identity type is invalid.");
            }

            return type.Equals(EIdentityType.CPF.ToString())
                ? EIdentityType.CPF
                : EIdentityType.RG;
        }

        public IdentityCard SetAsFavorite()
        {
            IsFavorite = true;
            return this;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Type;
            yield return Value;
            yield return Expiration;
        }
    }

    public enum EIdentityType
    {
        CPF = 0,
        RG = 1
    }
}
