using MediatR;
using TeleAppBot.Domain.Entities.Mensagens;
using TeleAppBot.Domain.Repositories;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem.EnviarMensagemMidia
{
    public class EnviarMensagemMidiaCommandHandler : IRequestHandler<EnviarMensagemMidiaCommand>
    {
        private readonly IMensagensRepository _mensagemRepository;
        private readonly IConversasRepository _conversasRepository;

        public EnviarMensagemMidiaCommandHandler(IMensagensRepository mensagemRepository, IConversasRepository conversasRepository)
        {
            _mensagemRepository = mensagemRepository;
            _conversasRepository = conversasRepository;
        }

        public async Task<Unit> Handle(EnviarMensagemMidiaCommand request, CancellationToken cancellationToken)
        {
            if (request.Tipo != TipoMensagem.Midia)
            {
                throw new Exception("Tipo de Mensagem não é mídia.");
            }

            var conversa = await _conversasRepository.ObterPorIdChat(request.IdChat);
            if (conversa is null)
            {
                throw new Exception("Chat informado não existe");
            }

            var mensagem = new MensagemMidia(request.idMensagem, request.Tipo, conversa.Id, request.Data, request.IdArquivo, request.IdUnicoArquivo, request.Tamanho);

            await _mensagemRepository.Salvar(mensagem);

            return Unit.Value;
        }
    }
}
