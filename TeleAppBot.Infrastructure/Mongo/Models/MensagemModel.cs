using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Infrastructure.Mongo.Models
{
    public class MensagemModel
    {
        public Guid Id { get; set; }
        public int IdMensagem { get; set; }

        public TipoMensagem Tipo { get; set; }

        public Guid IdConversa { get; set; }

        public DateTime Data { get; set; }

        public MensagemTexto? Texto { get; set; }
        public MensagemMidia? Midia { get; set; }
    }
}
