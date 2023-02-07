using TeleAppBot.Domain.Entities.Mensagens;

namespace TeleAppBot.Domain.Entities.Conversas
{
    public class Conversa : Entity, IAggregateRoot
    {
        public int IdChat { get; private set; }
        public Guid IdContato { get; private set; }
        public IList<Mensagem> _mensagens { get; }

        public IReadOnlyList<Mensagem> Mensagens => _mensagens.AsReadOnly();

        public Conversa(Guid idContato, IList<Mensagem> mensagens)
        {
            IdContato = idContato;
            _mensagens = mensagens;
        }
    }
}
