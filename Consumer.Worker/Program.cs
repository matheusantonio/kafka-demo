using Consumer.CrossCutting;
using Consumer.Domain.Events;
using Consumer.Domain.Handlers.Events;
using Consumer.Infrastructure.ExternalServices.Kafka;
using Consumer.Infrastructure.Persistence.Core.Mongo;
using Consumer.Infrastructure.Routers;
using Consumer.Worker;
using Shared.Events;
using Shared.Events.Handlers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddOptions();
        services.Configure<KafkaSettings>(context.Configuration.GetSection(KafkaSettings.Kafka));
        services.Configure<MongoSettings>(context.Configuration.GetSection(nameof(MongoSettings)));

        services.ConfigureMongo();

        services.AddScoped<IEventRouter, EventRouter>();

        services.AddScoped<IEventHandler<MessageCreatedEvent>, MessageEventHandler>();
        services.AddScoped<IEventHandler<MessageRemovedEvent>, MessageEventHandler>();

        services.AddHostedService<Worker<MessageCreatedEvent>>();
        services.AddHostedService<Worker<MessageRemovedEvent>>();
    })
    .Build();

await host.RunAsync();
