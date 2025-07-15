using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Social.Domain.Models;
using Social.Services.Services;


namespace Social.Services.Kafka
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<KafkaConsumerService> _logger;

        public KafkaConsumerService(IServiceScopeFactory scopeFactory,ILogger<KafkaConsumerService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:29092",
                GroupId = "user-registration-consumer",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("user-created");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var message = _consumer.Consume(stoppingToken);
                    await ProcessMessageAsync(message.Message.Value, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private async Task ProcessMessageAsync(string message, CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var clientService = scope.ServiceProvider.GetRequiredService<IClientService>();

            string[] parts = message.Split(',');
            if (parts.Length < 2) return;

            var userIdPart = parts[0].Trim();
            var namePart = parts[1].Trim();

            if (!userIdPart.StartsWith("user-id :") || !namePart.StartsWith("Name :"))
                return;

            var userIdString = userIdPart["user-id :".Length..].Trim();
            var name = namePart["Name :".Length..].Trim();

            if (!Guid.TryParse(userIdString, out var userId))
            {
                _logger.LogWarning("Invalid GUID format in message: {UserIdString}", userIdString);
                return;
            }

            try
            {
                var result = await clientService.AddAsync(
                    new ClientAddRequest { Id = userId, Name = name },
                    stoppingToken);

                _logger.LogInformation("Successfully processed user {UserId}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing user {UserId}", userId);
            }
        }

        public override void Dispose()
        {
            _consumer?.Close();
            _consumer?.Dispose();
            base.Dispose();
        }
    }
}