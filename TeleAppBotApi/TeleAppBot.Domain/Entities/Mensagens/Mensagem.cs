using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Domain.Entities.Mensagens
{
    public class Mensagem : Entity, IAggregateRoot
    {
        private Mensagem(int idMensagem, TipoMensagem tipo, Guid idConversa, DateTime data)
        {
            IdMensagem = idMensagem;
            Tipo = tipo;
            IdConversa = idConversa;
            Data = data;
        }

        public Mensagem(int idMensagem, TipoMensagem tipo, Guid idConversa, DateTime data, string texto) : 
            this(idMensagem, tipo, idConversa, data)
        {
            Texto = new MensagemTexto(texto);
            Midia = null;
        }

        public Mensagem(int idMensagem, TipoMensagem tipo, Guid idConversa, DateTime data, string idArquivo, string idUnicoArquivo, int tamanho) :
            this(idMensagem, tipo, idConversa, data)
        {
            Texto = null;
            Midia = new MensagemMidia(idArquivo, idUnicoArquivo, tamanho);
        }

        public int IdMensagem { get; private set; }

        public TipoMensagem Tipo { get; private set; }

        public Guid IdConversa { get; private set; }

        public DateTime Data { get; private set; }

        public MensagemTexto? Texto { get; private set; }
        public MensagemMidia? Midia { get; private set; }
    }
}
