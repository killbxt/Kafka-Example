using Social.Domain.Models;

namespace Social.Persistance.Repositories
{
    public interface IClientRepository
    {
        Task<Client> AddAsync(Client client,CancellationToken cancellation);
        Task<Client> GetById(Guid id,CancellationToken cancellation);
    }
}
