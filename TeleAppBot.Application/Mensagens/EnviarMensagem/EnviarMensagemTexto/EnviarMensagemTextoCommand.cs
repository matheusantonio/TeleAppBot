using MediatR;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem.EnviarMensagemTexto
{
    public record EnviarMensagemTextoCommand(int idMensagem, int IdChat, TipoMensagem Tipo, DateTime Data, string Texto) : EnviarMensagemCommand(IdChat, Tipo, Data), IRequest;
}
