using Auth.Domain.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Auth.Services.Kafka
{
    public class KafkaProducerService
    {
        private readonly IProducer<string, string> _producer;
        public KafkaProducerService(IOptions<KafkaProducerConfig> config)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config.Value.BootstrapServers,
            };
            var builder = new ProducerBuilder<string, string>(producerConfig)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8);

            _producer = builder.Build();
        }

        public async Task ProduceAsync(Guid key,string topic, string message,CancellationToken cancellation)
        {
            await _producer.ProduceAsync(topic, new Message<string, string> {Key = key.ToString(), Value = message }, cancellation);
        }

        public void Dispose() => _producer.Dispose();
    }
}
