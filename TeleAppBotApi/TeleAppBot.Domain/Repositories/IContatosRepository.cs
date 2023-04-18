using TeleAppBot.Domain.Entities.Contatos;

namespace TeleAppBot.Domain.Repositories
{
    public interface IContatosRepository : IRepository<Contato>
    {
        Task<Contato> ObterPorIdContato(long idContato);
    }
}
