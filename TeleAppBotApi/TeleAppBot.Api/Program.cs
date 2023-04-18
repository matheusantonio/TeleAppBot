using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text.Json;
using TeleAppBot.Application.Conversas.InverterConversa;
using TeleAppBot.Application.Conversas.ObterConversa;
using TeleAppBot.Application.Mensagens.EnviarMensagem;
using TeleAppBot.CrossCutting.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AdicionarServicos(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options => options.AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/mensagem", async ([FromBody] EnviarMensagemCommand command, [FromServices] IMediator mediator) =>
{
    Console.WriteLine($"Requisição recebida: {JsonSerializer.Serialize(command)}");
    await mediator.Send(command);
})
.WithName("EnviarMensagem")
.WithOpenApi();

app.MapGet("/conversa/{idChat}", async (long idChat, [FromServices] IMediator mediator) =>
{
    Console.WriteLine($"Requisição recebida: {idChat}");
    return await mediator.Send(new ObterConversaQuery(idChat));
})
.WithName("ObterConversa")
.WithOpenApi();

app.MapPost("/conversa", async ([FromBody] InverterConversaCommand command, [FromServices] IMediator mediator) =>
{
    await mediator.Send(command);
})
.WithName("InverterConversa")
.WithOpenApi();

app.Run();