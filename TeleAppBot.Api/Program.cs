using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TeleAppBot.Application.Mensagens.EnviarMensagem;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/mensagem", ([FromBody] EnviarMensagemCommand command, [FromServices] IMediator mediator) =>
{
    mediator.Send(command);
})
.WithName("EnviarMensagem")
.WithOpenApi();


app.Run();