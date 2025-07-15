using Auth.Domain.Models;
using Auth.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest, CancellationToken cancellation)
        {
            var result = await _service.Register(registerRequest,cancellation);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result.ErrorMessage);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellation)
        {
            var result = await _service.Login(loginRequest, cancellation);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result.ErrorMessage);
        }
    }
}
