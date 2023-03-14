using AutoMapper;
using MongoDB.Driver;
using TeleAppBot.Domain.Entities.Mensagens;
using TeleAppBot.Domain.Repositories;
using TeleAppBot.Infrastructure.Mongo.Models;

namespace TeleAppBot.Infrastructure.Mongo.Repositories
{
    public class MensagensRepository : IMensagensRepository
    {
        private readonly Context _contexto;
        private readonly IMapper _autoMapper;

        public MensagensRepository(Context contexto, IMapper autoMapper)
        {
            _contexto = contexto;
            _autoMapper = autoMapper;
        }

        public async Task Atualizar(Mensagem aggregateRoot)
        {
            var mensagemModel = _autoMapper.Map<MensagemModel>(aggregateRoot);
            await _contexto.Mensagens.ReplaceOneAsync(m => m.Id == mensagemModel.Id, mensagemModel);
        }

        public async Task<Mensagem> Obter(Guid Id)
        {
            var mensagemModel = await _contexto.Mensagens.Find(m => m.Id == Id).FirstOrDefaultAsync();
            return _autoMapper.Map<Mensagem>(mensagemModel);
        }

        public async Task Remover(Guid Id)
        {
            await _contexto.Mensagens.DeleteOneAsync(m => m.Id == Id);
        }

        public async Task Salvar(Mensagem aggregateRoot)
        {
            var mensagemModel = _autoMapper.Map<MensagemModel>(aggregateRoot);
            await _contexto.Mensagens.InsertOneAsync(mensagemModel);
        }
    }
}
