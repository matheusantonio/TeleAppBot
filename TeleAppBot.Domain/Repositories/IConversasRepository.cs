using TeleAppBot.Domain.Entities.Conversas;

namespace TeleAppBot.Domain.Repositories
{
    public interface IConversasRepository : IRepository<Conversa>
    {
        Task<Conversa> ObterPorIdChat(long idChat);
    }
}
