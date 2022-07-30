using Shared.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Publisher.Domain.Commands;
using Shared.Events;
using Shared.Commands.Handlers;
using Publisher.Domain.Handlers.Commands;
using Shared.Events.Handlers;
using Publisher.Domain.Events;
using Publisher.Domain.Handlers.Events;
using Publisher.Infrastructure.Routers;
using Publisher.Infrastructure.ExternalServices.Kafka;
using Publisher.Infrastructure.Persistence.Mongo;
using MongoDB.Driver;
using Publisher.Domain.Repositories;
using Publisher.Infrastructure.Repositories;
using Shared.ExternalServices.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOptions();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(KafkaSettings.Kafka));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection(nameof(MongoSettings)));

builder.Services.AddScoped<IMongoDatabase>(x =>
{
    var mongoSettings = builder.Configuration.GetSection(nameof(MongoSettings));
    var connectionString = mongoSettings.GetSection("ConnectionString").Value;
    var databaseName = mongoSettings.GetSection("DatabaseName").Value;
    var client = new MongoClient(connectionString);
    return client.GetDatabase(databaseName);
});

builder.Services.AddScoped<ICommandRouter, CommandRouter>();
builder.Services.AddScoped<IEventRouter, EventRouter>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IEventProducer, KafkaProducer>();

builder.Services.AddScoped<ICommandHandler<CreateMessageCommand>, MessageCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveMessageCommand>, MessageCommandHandler>();

builder.Services.AddScoped<IEventHandler<MessageUpvotedEvent>, RatingsEventHandler>();
builder.Services.AddScoped<IEventHandler<MessageDownvotedEvent>, RatingsEventHandler>();

builder.Services.AddSingleton<IHostedService, KafkaConsumer<MessageUpvotedEvent>>();
builder.Services.AddSingleton<IHostedService, KafkaConsumer<MessageDownvotedEvent>>();

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


app.MapGet("/", () =>
{
    return "";
});

app.MapPost("/message", async ([FromServices] ICommandRouter commandRouter,
                         [FromBody] CreateMessageCommand command) =>
{
    await commandRouter.Send(command);
});

app.MapDelete("/message/{messageId}", async ([FromServices] ICommandRouter commandRouter, 
                                       Guid messageId) =>
{
    await commandRouter.Send(new RemoveMessageCommand { MessageId = messageId });
});

app.Run();
