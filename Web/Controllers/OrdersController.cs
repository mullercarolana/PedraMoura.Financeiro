using ApplicationCore.Command;
using ApplicationCore.Entities.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMediator _mediator;
        public OrdersController(IOrdersRepository ordersRepository, IMediator mediator)
        {
            _ordersRepository = ordersRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder(PostOrderInputModel inputModel)
        {
            var command = PostOrderCommand.Create(
                inputModel.ClientId,
                AddressCommand.Create(
                    inputModel.Address.FavoriteAddress,
                    inputModel.Address.AddressId),
                inputModel.Products.Select(p =>
                    ProductCommand.Create(
                        p.ProductId,
                        p.Amount))
                    .ToList(),
                inputModel.TypeOfPayment);

            return await _mediator.Send(command) is var result && result.IsError
                ? BadRequest(result.Error)
                : Ok(result.Success);
        }

        [HttpGet("Payment/Pix/{txid}")]
        public async Task<IActionResult> GetPaymentOrder([FromRoute] string txid)
        {
            return await _ordersRepository.GetPixAsync(txid) is var result && result.IsError
                ? BadRequest(result.Error)
                : Ok(result.Success);
        }
    }
}
