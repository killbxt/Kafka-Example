using Social.Domain.Models;
using Social.Persistance.Repositories;

namespace Social.Services.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Client>> AddAsync(ClientAddRequest clientAddRequest, CancellationToken cancellation)
        {
            var result = await _repository.AddAsync(new Client { Id = clientAddRequest.Id, Name = clientAddRequest.Name}, cancellation);

            if (result != null) 
            {
                return Result<Client>.Success(result);       
            }
            return Result<Client>.Failure("Ошибка создания в основном микросервисе");
        }

        public async Task<Result<Client>> GetById(Guid id, CancellationToken cancellation)
        {
            var result = await _repository.GetById(id, cancellation);
            if (result != null)
            {
                return Result<Client>.Success(result);
            }
            return Result<Client>.Failure("Ошибка поиска в основном микросервисе");
        }
    }
}
