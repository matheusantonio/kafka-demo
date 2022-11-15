using Shared.Commands;
using Microsoft.AspNetCore.Mvc;
using Publisher.Domain.Commands;
using Shared.Commands.Handlers;
using Publisher.Domain.Handlers.Commands;
using Publisher.Infrastructure.Routers;
using Publisher.Infrastructure.ExternalServices.Kafka;
using Publisher.Infrastructure.Persistence.Mongo;
using Publisher.Domain.Repositories;
using Shared.ExternalServices.Events;
using Publisher.CrossCutting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOptions();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(KafkaSettings.Kafka));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection(nameof(MongoSettings)));

builder.Services.ConfigureMongo();

builder.Services.AddScoped<ICommandRouter, CommandRouter>();

builder.Services.AddScoped<IEventProducer, KafkaProducer>();

builder.Services.AddScoped<ICommandHandler<CreateMessageCommand>, MessageCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveMessageCommand>, MessageCommandHandler>();

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
