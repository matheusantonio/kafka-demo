using Confluent.Kafka;
using Consumer.Infrastructure.ExternalServices.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Shared.Events;

namespace Consumer.Worker
{
    public class Worker<T> : BackgroundService where T : IDomainEvent
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IOptions<KafkaSettings> _settings;

        public Worker(IOptions<KafkaSettings> settings, IServiceScopeFactory serviceScopeFactory)
        {
            _settings = settings;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () =>
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
                        var topic = _settings.Value.Topics.FirstOrDefault(x => x.Key == typeof(T).Name).Value;

                        if (string.IsNullOrEmpty(topic)) await Task.CompletedTask;

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
                                    await eventRouter.Send(orderRequest);
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

                await Task.CompletedTask;
            });
        }
    }
}