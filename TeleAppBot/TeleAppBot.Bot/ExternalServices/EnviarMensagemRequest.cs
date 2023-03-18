namespace TeleAppBot.Bot.ExternalServices
{
    public record EnviarMensagemRequest(int IdMensagem, long IdChat, long IdContato, TipoMensagem Tipo, DateTime Data, InformacoesContatoRequest Contato, MensagemTextoRequest? MensagemTexto, MensagemMidiaRequest? MensagemMidia);

    public record InformacoesContatoRequest(bool EBot, string Nome, string Sobrenome, string Usuario);

    public record MensagemTextoRequest(string Texto);

    public record MensagemMidiaRequest(string IdArquivo, string IdUnicoArquivo, long Tamanho);

    public enum TipoMensagem
    {
        Texto = 0,
        Midia = 1
    }

}
