using AutoMapper;
using MongoDB.Driver;
using TeleAppBot.Domain.Entities.Conversas;
using TeleAppBot.Domain.Repositories;
using TeleAppBot.Infrastructure.Mongo.Models;

namespace TeleAppBot.Infrastructure.Mongo.Repositories
{
    public class ConversasRepository : IConversasRepository
    {
        private readonly Context _contexto;
        private readonly IMapper _autoMapper;

        public ConversasRepository(Context contexto, IMapper autoMapper)
        {
            _contexto = contexto;
            _autoMapper = autoMapper;
        }

        public async Task Atualizar(Conversa aggregateRoot)
        {
            var conversaModel = _autoMapper.Map<ConversaModel>(aggregateRoot);
            await _contexto.Conversas.ReplaceOneAsync(c => c.Id == conversaModel.Id, conversaModel);
        }

        public async Task<Conversa> Obter(Guid Id)
        {
            var conversaModel = await _contexto.Conversas.Find(m => m.Id == Id).FirstOrDefaultAsync();
            return _autoMapper.Map<Conversa>(conversaModel);
        }

        public async Task<Conversa> ObterPorIdChat(long idChat)
        {
            var conversaModel = await _contexto.Conversas.Find(m => m.IdChat == idChat).FirstOrDefaultAsync();
            return _autoMapper.Map<Conversa>(conversaModel);
        }

        public async Task Remover(Guid Id)
        {
            await _contexto.Conversas.DeleteOneAsync(m => m.Id == Id);
        }

        public async Task Salvar(Conversa aggregateRoot)
        {
            var conversaModel = _autoMapper.Map<ConversaModel>(aggregateRoot);
            await _contexto.Conversas.InsertOneAsync(conversaModel);
        }
    }
}
