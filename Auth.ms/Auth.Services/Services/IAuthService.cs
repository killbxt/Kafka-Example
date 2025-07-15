using Auth.Domain.Models;

namespace Auth.Services.Services
{
    public interface IAuthService
    {
        Task<Result<string>> Login(LoginRequest loginRequest, CancellationToken cancellation);
        Task<Result<string>> Register(RegisterRequest registerRequest, CancellationToken cancellation);
    }
}
