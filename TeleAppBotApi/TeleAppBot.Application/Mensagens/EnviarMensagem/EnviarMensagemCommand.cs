using MediatR;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem
{
    public record EnviarMensagemCommand(int IdMensagem, long IdChat, long IdContato, TipoMensagem Tipo, DateTime Data, InformacoesContatoCommand Contato, EnviarMensagemTextoCommand? MensagemTexto, EnviarMensagemMidiaCommand? MensagemMidia) : IRequest<Unit>;
}
