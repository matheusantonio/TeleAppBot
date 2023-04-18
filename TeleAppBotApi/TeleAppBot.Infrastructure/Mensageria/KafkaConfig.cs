namespace TeleAppBot.Infrastructure.Mensageria
{
    public class KafkaConfig
    {
        public string Broker { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConsumerGroup { get; set; }

        public string TopicoMensagemTexto { get; set; }
        public string TopicoMensagemMidia { get; set; }
    }
}
