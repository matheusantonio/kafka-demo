using Publisher.CrossCutting;
using Publisher.Domain.Events;
using Publisher.Domain.Handlers.Events;
using Publisher.Infrastructure.ExternalServices.Kafka;
using Publisher.Infrastructure.Persistence.Mongo;
using Publisher.Infrastructure.Routers;
using Publisher.Worker;
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

        services.AddScoped<IEventHandler<MessageUpvotedEvent>, RatingsEventHandler>();
        services.AddScoped<IEventHandler<MessageDownvotedEvent>, RatingsEventHandler>();

        services.AddHostedService<Worker<MessageUpvotedEvent>>();
        services.AddHostedService<Worker<MessageDownvotedEvent>>();
    })
        .Build();

await host.RunAsync();
