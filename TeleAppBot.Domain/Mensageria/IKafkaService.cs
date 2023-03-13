namespace TeleAppBot.Domain.Mensageria
{
    public interface IKafkaService
    {
        Task EnviarMensagem<T>(T message) where T : Evento;

        Task<T> ConsumirMensagem<T>() where T : Evento;
    }
}
