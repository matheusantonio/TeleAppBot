namespace TeleAppBot.Infrastructure.Mensageria
{
    public class KafkaConfig
    {
        public string BootstrapServer { get; set; }
        public string ConsumerGroup { get; set; }

        public string TopicoMensagemTexto { get; set; }
        public string TopicoMensagemMidia { get; set; }
    }
}
