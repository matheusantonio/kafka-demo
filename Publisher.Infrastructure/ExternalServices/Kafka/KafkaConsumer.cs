using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Shared.Events;

namespace Publisher.Infrastructure.ExternalServices.Kafka
{
    public class KafkaConsumer<T>: IHostedService where T : IDomainEvent
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IOptions<KafkaSettings> _settings;

        public KafkaConsumer(IServiceScopeFactory serviceScopeFactory, IOptions<KafkaSettings> settings)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _settings = settings;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _settings.Value.GroupId,
                BootstrapServers = _settings.Value.BootstrapServer,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                    <Ignore, string>(config).Build())
                {
                    var topic = _settings.Value.Topics.FirstOrDefault(x => x.Key == nameof(T)).Value;

                    if (string.IsNullOrEmpty(topic)) await Task.CompletedTask;

                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while(true)
                        {
                            var consumer = consumerBuilder.Consume
                                (cancelToken.Token);
                            var orderRequest = JsonSerializer.Deserialize<T>(consumer.Message.Value);

                            //event handler
                            using(var scope = _serviceScopeFactory.CreateScope())
                            {
                                var eventRouter = scope.ServiceProvider.GetService<IEventRouter>();
                                await eventRouter.Send(orderRequest);
                            }
                        }
                    }
                    catch(OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
