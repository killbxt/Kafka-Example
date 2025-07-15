using Auth.Domain.Models;
using Auth.Persistance.Repositories;
using Auth.Services.Kafka;

namespace Auth.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly KafkaProducerService _producer;

        public AuthService(IAuthRepository repository, KafkaProducerService producer)
        {
            _repository = repository;
            _producer = producer;
        } 
        public async Task<Result<string>> Login(LoginRequest loginRequest, CancellationToken cancellation)
        {
            var result = await _repository.Login(loginRequest, cancellation);

            if(result != string.Empty)
            {

                return Result<string>.Success(result);
            }
            return Result<string>.Failure("ошибка");
        }

        public async Task<Result<string>> Register(RegisterRequest registerRequest, CancellationToken cancellation)
        {
            var result = await _repository.Register(registerRequest, cancellation);

            if (result != string.Empty)
            {
                await _producer.ProduceAsync(Guid.NewGuid(),"user-created", $"user-id : {result}, Name : {registerRequest.Name}", cancellation);
                return Result<string>.Success(result);
            }
            return Result<string>.Failure("ошибка");
        }
    }
}
