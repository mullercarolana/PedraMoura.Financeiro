using ApplicationCore.Command;
using ApplicationCore.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMediator _mediator;
        public ProductsController(IProductsRepository productsRepository, IMediator mediator)
        {
            _productsRepository = productsRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(PostProductInputModel inputModel)
        {
            var command = PostProductCommand.Create(inputModel.Name, inputModel.Price, inputModel.MassMeasure, inputModel.Value);
            return await _mediator.Send(command) is var result && result.IsError
                ? BadRequest(result.Error)
                : Ok(result.Success);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            return await _productsRepository.GetProductsAsync(id) is var result && result.IsError
                ? BadRequest(result.Error)
                : Ok(result.Success);
        }
    }
}
