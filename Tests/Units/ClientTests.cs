using ApplicationCore.Entities.Clients;
using ApplicationCore.ValuesObjects;
using Shouldly;
using Xunit;

namespace Tests.Units
{
    public sealed class ClientTests
    {
        private readonly string _email = "anamullerdev@gmail.com";
        private readonly string _name = "Ana Carolina";
        private readonly string _phone = "51997362630";

        [Fact]
        public void GivenCorrectParameters_WhenCreatingClient_ReturnSuccess()
        {
            var address = Address.Create("Rua Farroupilha", "300", "Centro", "Estância Velha", "RS", "Brasil", "93600240");
            var identityCard = IdentityCard.Create("CPF", "03858963000", new DateTime(2028, 10, 05));
            identityCard.IsSuccess.ShouldBeTrue();
            var client = Client.Create(_name, _phone, _email, address, identityCard.Success);
            client.IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void GivenIncorrectParameters_WhenCreatingClien_NoAddress_ReturnError()
        {
            var client = Client.Create(_name, _phone, _email, null, null);
            client.IsError.ShouldBeTrue();
            client.Error.Message.ShouldBe("Address cannot to be null.");
            client.Error.Message.ShouldBe("Address cannot to be null.");
        }
    }
}
