using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem
{
    public record EnviarMensagemCommand(int IdChat, TipoMensagem Tipo, DateTime Data);

    

}
