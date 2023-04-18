using TeleAppBot.Domain.Mensageria;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Domain.Events
{
    public record EnviarMensagemMidiaEvent(int IdMensagem, long IdChat, long IdContato, TipoMensagem Tipo, DateTime Data, string IdArquivo, string IdUnicoArquivo, int Tamanho) : Evento;
}
