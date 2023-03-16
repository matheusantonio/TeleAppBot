using MediatR;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem
{
    public record EnviarMensagemCommand(int IdMensagem, int IdChat, int IdContato, TipoMensagem Tipo, DateTime Data, InformacoesContatoCommand Contato, EnviarMensagemTextoCommand? MensagemTexto, EnviarMensagemMidiaCommand? MensagemMidia) : IRequest<Unit>;
}
