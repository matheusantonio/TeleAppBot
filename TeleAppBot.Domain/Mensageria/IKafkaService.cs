namespace TeleAppBot.Domain.Mensageria
{
    public interface IKafkaService
    {
        Task EnviarMensagem<T>(T message) where T : Evento;

        T ConsumirMensagem<T>(CancellationToken tokenCancelamento = default) where T : Evento;
    }
}
