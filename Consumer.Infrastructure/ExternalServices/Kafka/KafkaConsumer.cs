using Confluent.Kafka;
using Consumer.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Commands;
using Shared.Events;
using System.Text.Json;

namespace Consumer.Infrastructure.ExternalServices.Kafka
{
    public class KafkaConsumer<T>: IHostedService where T : IDomainEvent
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IOptions<KafkaConfig> _options;

        public KafkaConsumer(IOptions<KafkaConfig> options,IServiceScopeFactory serviceScopeFactory)
        {
            _options = options;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _options.Value.GroupId,
                BootstrapServers = _options.Value.BootstrapServer,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    var topic = _options.Value.Topics.FirstOrDefault(x => x.Key == nameof(T)).Value;

                    if (string.IsNullOrEmpty(topic)) return Task.CompletedTask;

                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume
                               (cancelToken.Token);
                            var orderRequest = JsonSerializer.Deserialize<T>(consumer.Message.Value);

                            //event handler
                            using (var scope = _serviceScopeFactory.CreateScope())
                            {
                                var eventRouter = scope.ServiceProvider.GetService<IEventRouter>();
                                eventRouter.Send(orderRequest);
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
