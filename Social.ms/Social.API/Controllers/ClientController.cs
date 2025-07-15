using Microsoft.AspNetCore.Mvc;
using Social.Domain.Models;
using Social.Services.Services;

namespace Social.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Add(ClientAddRequest clientAddRequest, CancellationToken cancellation)
        {
            var result = await _clientService.AddAsync(clientAddRequest, cancellation);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellation)
        {
            var result = await _clientService.GetById(id, cancellation);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
