using TeleAppBot.Domain.Mensageria;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Domain.Events
{
    public record EnviarMensagemTextoEvent(int IdMensagem, int IdChat, int IdContato, InformacoesUsuarioEvent InformacoesUsuario, TipoMensagem Tipo, DateTime Data, string Texto) : Evento;
}
