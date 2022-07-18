using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.ExternalServices.Kafka;
using System.Net;

namespace Consumer.Infrastructure.ExternalServices.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly ProducerConfig _config;
        private readonly IOptions<KafkaSettings> _settings;

        public KafkaProducer(IOptions<KafkaSettings> settings)
        {
            _settings = settings;

            _config = new ProducerConfig
            {
                BootstrapServers = _settings.Value.BootstrapServer,
                ClientId = Dns.GetHostName()
            };
        }

        // Implementar criação automática de tópicos para não depender de um topico estar criado
        Task IKafkaProducer.CreateTopicMaybe(string topic)
        {
            throw new NotImplementedException();
        }

        public async Task Produce(string topic, object message)
        {
            try
            {
                using (var producer = new ProducerBuilder<Null, string>(_config).Build())
                {
                    var value = JObject.FromObject(message).ToString(Formatting.None);

                    var result = await producer.
                        ProduceAsync(topic, new Message<Null, string>
                        {
                            Value = value
                        });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
        }
    }
}
