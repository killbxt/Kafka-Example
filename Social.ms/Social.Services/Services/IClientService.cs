using Social.Domain.Models;

namespace Social.Services.Services
{
    public interface IClientService
    {
        Task<Result<Client>> AddAsync(ClientAddRequest clientAddRequest,CancellationToken cancellation);
        Task<Result<Client>> GetById(Guid id,CancellationToken cancellation);
    }
}
