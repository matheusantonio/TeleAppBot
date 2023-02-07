using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Domain.Entities.Mensagens
{
    public class MensagemTexto : Mensagem
    {
        public string Corpo { get; private set; }

        public MensagemTexto(int idMensagem, TipoMensagem tipo, Guid idConversa, DateTime data, string corpo) : base(idMensagem, tipo, idConversa, data)
        {
            Corpo = corpo;
        }
    }
}
