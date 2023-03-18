using TeleAppBot.Domain.Entities.Mensagens;

namespace TeleAppBot.Domain.Entities.Conversas
{
    public class Conversa : Entity, IAggregateRoot
    {
        public long IdChat { get; private set; }
        public Guid IdContato { get; private set; }
        public DateTime Data { get; private set; }
        public IList<Mensagem> _mensagens { get; }
        public bool Invertida { get; private set; }

        public IReadOnlyList<Mensagem> Mensagens => _mensagens.AsReadOnly();

        public Conversa(Guid idContato, int idChat, IList<Mensagem> mensagens) : this(idContato, idChat)
        {
            _mensagens = mensagens;
        }

        public Conversa(Guid idContato, int idChat)
        {
            IdContato = idContato;
            IdChat = idChat;
            _mensagens = new List<Mensagem>();

            Data = DateTime.Now;
            Invertida = false;
        }

        public void AdicionarMensagem(Mensagem mensagem)
        {
            _mensagens.Add(mensagem);
        }

        public void Inverter() => Invertida = true;

        public void Reverter() => Invertida = false;
    }
}
