namespace Auth.Domain.Models
{
    public class KafkaProducerConfig
    {
        public string BootstrapServers { get; set; } = "localhost:29092";
    }
}
