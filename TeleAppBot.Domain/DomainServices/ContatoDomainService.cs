using TeleAppBot.Domain.Repositories;
using TeleAppBot.Domain.Entities.Contatos;

namespace TeleAppBot.Domain.DomainServices
{
    public class ContatoDomainService : IContatoDomainService
    {
        private readonly IContatosRepository _contatosRepository;

        public ContatoDomainService(IContatosRepository contatosRepository)
        {
            _contatosRepository = contatosRepository;
        }

        public async Task ValidarExistenciaDeContato(int idUsuario, bool eBot, string nome, string sobrenome, string usuario)
        {
            var contato = await _contatosRepository.ObterPorIdContato(idUsuario);

            if (contato == null)
            {
                contato = new Contato(idUsuario, eBot, nome, sobrenome, usuario);

                await _contatosRepository.Salvar(contato);
                return;
            }

            if(contato.AtualizarContato(eBot, nome, sobrenome, usuario))
                await _contatosRepository.Atualizar(contato);
        }

        public async Task<bool> ValidarExistenciaDeContato(int idUsuario)
        {
            var contato = await _contatosRepository.ObterPorIdContato(idUsuario);

            return contato is not null;
        }
    }
}
