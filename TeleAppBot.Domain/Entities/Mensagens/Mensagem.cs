using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Domain.Entities.Mensagens
{
    public abstract class Mensagem : Entity, IAggregateRoot
    {
        protected Mensagem(int idMensagem, TipoMensagem tipo, Guid idConversa, DateTime data)
        {
            IdMensagem = idMensagem;
            Tipo = tipo;
            IdConversa = idConversa;
            Data = data;
        }

        public int IdMensagem { get; private set; }

        public TipoMensagem Tipo { get; private set; }

        public Guid IdConversa { get; private set; }

        public DateTime Data { get; private set; }
    }
}
