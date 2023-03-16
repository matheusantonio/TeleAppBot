using TeleAppBot.Domain.Entities.Conversas;
using TeleAppBot.Domain.Entities.Mensagens;
using TeleAppBot.Domain.Events;
using TeleAppBot.Domain.Repositories;

namespace TeleAppBot.Domain.DomainServices
{
    public class MensagemDomainService : IMensagemDomainService
    {
        private readonly IMensagensRepository _mensagensRepository;
        private readonly IConversasRepository _conversasRepository;
        private readonly IContatosRepository _contatosRepository;

        public MensagemDomainService(IMensagensRepository mensagensRepository, IConversasRepository conversasRepository, IContatosRepository contatosRepository)
        {
            _mensagensRepository = mensagensRepository;
            _conversasRepository = conversasRepository;
            _contatosRepository = contatosRepository;
        }

        public async Task ProcessarEnvioDeMensagem(EnviarMensagemTextoEvent evento)
        {
            var contato = await _contatosRepository.ObterPorIdContato(evento.IdContato);

            if(contato is null)
            {
                return;
            }

            var conversa = await _conversasRepository.ObterPorIdChat(evento.IdChat);

            if (conversa is null)
            {
                conversa = new Conversa(contato.Id, evento.IdChat);
                await _conversasRepository.Salvar(conversa);
            }

            var mensagem = new Mensagem(evento.IdMensagem, evento.Tipo, conversa.Id, evento.Data, evento.Texto);
            await _mensagensRepository.Salvar(mensagem);
        }

        public async Task ProcessarEnvioDeMidia(EnviarMensagemMidiaEvent evento)
        {
            var contato = await _contatosRepository.ObterPorIdContato(evento.IdContato);

            if (contato is null)
            {
                return;
            }

            var conversa = await _conversasRepository.ObterPorIdChat(evento.IdChat);

            if (conversa is null)
            {
                conversa = new Conversa(contato.Id, evento.IdChat);
                await _conversasRepository.Salvar(conversa);
            }

            var mensagem = new Mensagem(evento.IdMensagem, evento.Tipo, conversa.Id, evento.Data, evento.IdArquivo, evento.IdUnicoArquivo, evento.Tamanho);
            await _mensagensRepository.Salvar(mensagem);
        }
    }
}
