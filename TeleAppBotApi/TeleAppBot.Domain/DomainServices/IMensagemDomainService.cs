using TeleAppBot.Domain.Events;

namespace TeleAppBot.Domain.DomainServices
{
    public interface IMensagemDomainService
    {
        Task ProcessarEnvioDeMensagem(EnviarMensagemTextoEvent evento);

        Task ProcessarEnvioDeMidia(EnviarMensagemMidiaEvent evento);
    }
}
