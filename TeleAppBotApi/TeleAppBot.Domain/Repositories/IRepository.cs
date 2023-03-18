using TeleAppBot.Domain.Entities;

namespace TeleAppBot.Domain.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> Obter(Guid Id);

        Task Salvar(T aggregateRoot);

        Task Atualizar(T aggregateRoot);

        Task Remover(Guid Id);
    }
}
