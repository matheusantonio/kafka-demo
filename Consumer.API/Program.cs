using Consumer.CrossCutting;
using Consumer.Domain.Commands;
using Consumer.Domain.Handlers.Commands;
using Consumer.Domain.Repositories;
using Consumer.Infrastructure.ExternalServices.Kafka;
using Consumer.Infrastructure.Persistence.Core.Mongo;
using Consumer.Infrastructure.Routers;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Shared.Commands.Handlers;
using Shared.ExternalServices.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOptions();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(KafkaSettings.Kafka));

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection(nameof(MongoSettings)));

builder.Services.ConfigureMongo();

builder.Services.AddScoped<ICommandRouter, CommandRouter>();

builder.Services.AddScoped<IEventProducer, KafkaProducer>();

builder.Services.AddScoped<ICommandHandler<UpvoteMessageCommand>, RatingsCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DownvoteMessageCommand>, RatingsCommandHandler>();

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
