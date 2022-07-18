using Consumer.Domain.Commands;
using Consumer.Domain.Events;
using Consumer.Domain.Handlers.Commands;
using Consumer.Domain.Handlers.Events;
using Consumer.Infrastructure.ExternalServices.Kafka;
using Consumer.Infrastructure.Routers;
using Microsoft.AspNetCore.Mvc;
using Shared.Commands;
using Shared.Commands.Handlers;
using Shared.Events;
using Shared.Events.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOptions();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(KafkaSettings.Kafka));

builder.Services.AddScoped<ICommandRouter, CommandRouter>();
builder.Services.AddScoped<IEventRouter, EventRouter>();

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


app.MapGet("/messages", () =>
{
    return "";
});

app.MapPut("/message/{messageId}/upvote", ([FromServices] ICommandRouter commandRouter, Guid messageId) =>
{
    commandRouter.Send(new UpvoteMessageCommand
    {
        MessageId = messageId
    });
});

app.MapPut("/message/{messageId}/downvote", ([FromServices] ICommandRouter commandRouter, Guid messageId) =>
{
    commandRouter.Send(new DownvoteMessageCommand
    {
        MessageId = messageId
    });
});

app.Run();
