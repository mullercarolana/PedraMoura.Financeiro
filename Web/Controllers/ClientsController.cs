using ApplicationCore.Command;
using ApplicationCore.Entities.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IMediator _mediator;
        public ClientsController(IClientsRepository clientsRepository, IMediator mediator)
        {
            _clientsRepository = clientsRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostClient(PostClientInputModel inputModel)
        {
            var command = PostClientCommand.Create(
                inputModel.Name,
                inputModel.Phone,
                inputModel.Email,
                inputModel.Street,
                inputModel.Number,
                inputModel.Complement,
                inputModel.City,
                inputModel.State,
                inputModel.Country,
                inputModel.ZipCode,
                inputModel.Type,
                inputModel.Value,
                inputModel.Expiration);

            return await _mediator.Send(command) is var result && result.IsError
                ? BadRequest(result.Error)
                : Ok(result.Success);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            return await _clientsRepository.GetClientAsync(id) is var result && result.IsError
                ? BadRequest(result.Error)
                : Ok(result.Success);
        }
    }
}
