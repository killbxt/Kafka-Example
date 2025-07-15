using Auth.Domain.Models;

namespace Auth.Persistance.Repositories
{
    public interface IAuthRepository
    {
        Task<string> Login(LoginRequest loginRequest, CancellationToken cancellation); //jwt возвращает
        Task<string> Register(RegisterRequest registerRequest, CancellationToken cancellation);
    }
}
