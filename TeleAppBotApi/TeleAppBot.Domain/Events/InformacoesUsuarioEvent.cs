namespace TeleAppBot.Domain.Events
{
    public record InformacoesUsuarioEvent(bool EBot, string Nome, string Sobrenome, string Usuario);
}
