using AutoMapper;
using MongoDB.Driver;
using TeleAppBot.Domain.Entities.Contatos;
using TeleAppBot.Domain.Repositories;
using TeleAppBot.Infrastructure.Mongo.Models;

namespace TeleAppBot.Infrastructure.Mongo.Repositories
{
    public class ContatosRepository : IContatosRepository
    {
        private readonly Context _contexto;
        private readonly IMapper _autoMapper;

        public ContatosRepository(Context contexto, IMapper autoMapper)
        {
            _contexto = contexto;
            _autoMapper = autoMapper;
        }

        public async Task Atualizar(Contato aggregateRoot)
        {
            var contatoModel = _autoMapper.Map<ContatoModel>(aggregateRoot);
            await _contexto.Contatos.ReplaceOneAsync(c => c.Id == contatoModel.Id, contatoModel);
        }

        public async Task<Contato> Obter(Guid Id)
        {
            var contatoModel = await _contexto.Contatos.Find(m => m.Id == Id).FirstOrDefaultAsync();
            return _autoMapper.Map<Contato>(contatoModel);
        }

        public async Task<Contato> ObterPorIdContato(long idContato)
        {
            var contatoModel = await _contexto.Contatos.Find(m => m.IdUsuario == idContato).FirstOrDefaultAsync();
            return _autoMapper.Map<Contato>(contatoModel);
        }

        public async Task Remover(Guid Id)
        {
            await _contexto.Contatos.DeleteOneAsync(m => m.Id == Id);
        }

        public async Task Salvar(Contato aggregateRoot)
        {
            var contatoModel = _autoMapper.Map<ContatoModel>(aggregateRoot);
            await _contexto.Contatos.InsertOneAsync(contatoModel);
        }
    }
}
