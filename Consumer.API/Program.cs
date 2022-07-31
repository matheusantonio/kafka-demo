using Consumer.Domain.Commands;
using Consumer.Domain.Events;
using Consumer.Domain.Handlers.Commands;
using Consumer.Domain.Handlers.Events;
using Consumer.Domain.Repositories;
using Consumer.Infrastructure.ExternalServices.Kafka;
using Consumer.Infrastructure.Persistence.Core.Mongo;
using Consumer.Infrastructure.Repositories;
using Consumer.Infrastructure.Routers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Shared.Commands;
using Shared.Commands.Handlers;
using Shared.Events;
using Shared.Events.Handlers;
using Shared.ExternalServices.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOptions();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(KafkaSettings.Kafka));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection(nameof(MongoSettings)));

builder.Services.AddSingleton(x =>
{
    var configuration = x.GetService<IConfiguration>();
    var options = new MongoSettings();

    configuration.GetSection(nameof(MongoSettings)).Bind(options);

    return options;
});

builder.Services.AddSingleton<IMongoClient>(x =>
{
    var mongoSettings = x.GetService<MongoSettings>();

    return new MongoClient(mongoSettings.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(x =>
{
    var mongoSettings = x.GetService<MongoSettings>();
    var client = x.GetService<IMongoClient>();

    return client.GetDatabase(mongoSettings.DatabaseName);
});

builder.Services.AddScoped<ICommandRouter, CommandRouter>();
builder.Services.AddScoped<IEventRouter, EventRouter>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IEventProducer, KafkaProducer>();

builder.Services.AddScoped<ICommandHandler<UpvoteMessageCommand>, RatingsCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DownvoteMessageCommand>, RatingsCommandHandler>();

builder.Services.AddScoped<IEventHandler<MessageCreatedEvent>, MessageEventHandler>();
builder.Services.AddScoped<IEventHandler<MessageRemovedEvent>, MessageEventHandler>();

builder.Services.AddSingleton<IHostedService, KafkaConsumer<MessageCreatedEvent>>();
builder.Services.AddSingleton<IHostedService, KafkaConsumer<MessageRemovedEvent>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", async ([FromServices] IMessageRepository repository) =>
{
    return await repository.List();
});

app.MapPut("/message/{messageId}/upvote", async ([FromServices] ICommandRouter commandRouter, Guid messageId) =>
{
    await commandRouter.Send(new UpvoteMessageCommand
    {
        MessageId = messageId
    });
});

app.MapPut("/message/{messageId}/downvote", async ([FromServices] ICommandRouter commandRouter, Guid messageId) =>
{
    await commandRouter.Send(new DownvoteMessageCommand
    {
        MessageId = messageId
    });
});

app.Run();
