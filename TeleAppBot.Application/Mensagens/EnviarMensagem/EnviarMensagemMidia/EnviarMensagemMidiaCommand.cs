using MediatR;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem.EnviarMensagemMidia
{
    public record EnviarMensagemMidiaCommand(int idMensagem, int IdChat, TipoMensagem Tipo, DateTime Data, string IdArquivo, string IdUnicoArquivo, int Tamanho) : EnviarMensagemCommand(IdChat, Tipo, Data), IRequest;
}
