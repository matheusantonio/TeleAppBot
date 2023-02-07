using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Domain.Entities.Mensagens
{
    public class MensagemMidia : Mensagem
    {
        public MensagemMidia(int idMensagem, TipoMensagem tipo, Guid idConversa, DateTime data, string idArquivo, string idUnicoArquivo, int tamanho) : base(idMensagem, tipo, idConversa, data)
        {
            IdArquivo = idArquivo;
            IdUnicoArquivo = idUnicoArquivo;
            Tamanho = tamanho;
        }

        public string IdArquivo { get; private set; }
        public string IdUnicoArquivo { get; private set; }
        public int Tamanho { get; private set; }
    }
}
