using TeleAppBot.Domain.Mensageria;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Domain.Events
{
    public record EnviarMensagemTextoEvent(int IdMensagem, long IdChat, long IdContato, InformacoesUsuarioEvent InformacoesUsuario, TipoMensagem Tipo, DateTime Data, string Texto) : Evento;
}
