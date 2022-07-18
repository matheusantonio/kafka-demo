using Shared.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Publisher.Domain.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapPost("/message", ([FromServices] ICommandRouter commandRouter,
                         [FromBody] CreateMessageCommand command) =>
{
    commandRouter.Send(command);
});

app.MapDelete("/message/{messageId}", ([FromServices] ICommandRouter commandRouter, 
                                       Guid messageId) =>
{
    commandRouter.Send(new RemoveMessageCommand { MessageId = messageId });
});

app.Run();
