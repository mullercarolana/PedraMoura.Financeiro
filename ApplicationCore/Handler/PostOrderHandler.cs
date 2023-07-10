using ApplicationCore.Command;
using ApplicationCore.Entities.Clients;
using ApplicationCore.Entities.Orders;
using ApplicationCore.Entities.Products;
using ApplicationCore.Seedwork.Exceptions;
using ApplicationCore.Services;
using ApplicationCore.Shared.Certificates;
using ApplicationCore.ValuesObjects;
using MediatR;

namespace ApplicationCore.Handler
{
    public sealed class PostOrderHandler : IRequestHandler<PostOrderCommand, Result<Order>>
    {
        private readonly IHttpRequestAuthenticateService _authenticateService;
        private readonly IProductsRepository _productsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IHttpRequestPixService _pixService;

        public PostOrderHandler(
            IHttpRequestAuthenticateService authenticateService,
            IProductsRepository productsRepository,
            IClientsRepository clientsRepository,
            IOrdersRepository ordersRepository,
            IHttpRequestPixService pixService
        )
        {
            _authenticateService = authenticateService;
            _productsRepository = productsRepository;
            _clientsRepository = clientsRepository;
            _ordersRepository = ordersRepository;
            _pixService = pixService;
        }

        public async Task<Result<Order>> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _clientsRepository.GetClientAsync(request.ClientId) is var client && client.IsError)
                {
                    return client.Error;
                }

                Address address = null;
                if (request.Address.FavoriteAddress && client.Success.GetFavoriteAdress() is var favorite && favorite.IsSuccess)
                {
                    address = favorite.Success;
                }
                else if (request.Address.AddressId.HasValue && client.Success.GetAddress(request.Address.AddressId.Value) is var otherAddress && otherAddress.IsSuccess)
                {
                    address = otherAddress.Success;
                }
                else if (address is null)
                {
                    return Error.New("AddressNotFound", "Address cannot to find.");
                }

                var productOrders = new List<ProductOrder>(capacity: request.Products.Count);
                foreach (var item in request.Products)
                {
                    if (await _productsRepository.GetProductsAsync(item.ProductId) is var product && product.IsError)
                    {
                        return product.Error;
                    }
                    productOrders.Add(ProductOrder.Create(product.Success, item.Amount));
                }

                var order = Order.CreateInProgress(client.Success, address.Id, productOrders);
                var paymentOrder = Pix.CreateInProcessing();
                order.SetInProgressPayment(paymentOrder);
                _ordersRepository.Add(order);
                await _ordersRepository.SaveChangesAsync();

                if (Order.GetTypePayment(request.TypeOfPayment) is var typePayment && typePayment.IsError)
                {
                    await SetErrorAsync(paymentOrder, order, typePayment.Error.ToString());
                    return typePayment.Error;
                }

                switch (typePayment.Success)
                {
                    case ETypePayment.Pix:
                        if (await _authenticateService.GetOAuthToken() is var authenticate && authenticate.IsError)
                        {
                            await SetErrorAsync(paymentOrder, order, authenticate.Error.ToString());
                            return authenticate.Error;
                        }

                        if (CriarBody(client.Success, order) is var body && body.IsError)
                        {
                            await SetErrorAsync(paymentOrder, order, body.Error.ToString());
                            return body.Error;
                        }

                        if (await _pixService.GeneratePixPaymentAsync(authenticate.Success, body.Success) is var pixPayment && pixPayment.IsError)
                        {
                            await SetErrorAsync(paymentOrder, order, pixPayment.Error.ToString());
                            return pixPayment.Error;
                        }
                        else
                        {
                            paymentOrder.SetApproved(pixPayment.Success);
                            order.SetSuccessfulPayment(paymentOrder);
                            await _ordersRepository.SaveChangesAsync();
                            return order;
                        }
                    default:
                        return Error.New("ErrorTypePayment", "Type payment is invalid.");
                }
            }
            catch (Exception ex)
            {
                return Error.New("ErrorPostOrder", $"Error to post the order. Error: {ex.Message}");
            }
        }

        private static Result<object> CriarBody(Client client, Order order)
        {
            if (client.GetFavoriteIdentityCard() is var identityCard && identityCard.IsError)
            {
                return identityCard.Error;
            }

            var valor = $"{Math.Round(order.TotalAmountPayable, 2)}".Replace(',', '.');
            return new
            {
                calendario = new { expiracao = 3600 },
                devedor = new { cpf = identityCard.Success.Value, nome = client.Name },
                valor = new { original = valor },
                chave = ClientCertificate.Key,
                solicitacaoPagador = "Informe o número ou identificador do pedido."
            };
        }

        private async Task SetErrorAsync(PaymentOrder paymentOrder, Order order, string error)
        {
            ((Pix)paymentOrder).SetCanceled(error);
            order.SetCanceledPayment();
            await _ordersRepository.SaveChangesAsync();
        }
    }
}
