using MediatR;
using TeleAppBot.Domain.Entities.Mensagens;
using TeleAppBot.Domain.Repositories;
using TeleAppBot.Domain.ValueObjects;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem.EnviarMensagemTexto
{
    public class EnviarMensagemTextoCommandHandler : IRequestHandler<EnviarMensagemTextoCommand>
    {
        private readonly IMensagensRepository _mensagemRepository;
        private readonly IConversasRepository _conversasRepository;

        public EnviarMensagemTextoCommandHandler(IMensagensRepository mensagemRepository, IConversasRepository conversasRepository)
        {
            _mensagemRepository = mensagemRepository;
            _conversasRepository = conversasRepository;
        }

        public async Task<Unit> Handle(EnviarMensagemTextoCommand request, CancellationToken cancellationToken)
        {
            if(request.Tipo != TipoMensagem.Texto)
            {
                throw new Exception("Tipo de Mensagem não é texto.");
            }

            var conversa = await _conversasRepository.ObterPorIdChat(request.IdChat);
            if(conversa is null)
            {
                throw new Exception("Chat informado não existe");
            }

            var mensagem = new MensagemTexto(request.idMensagem, request.Tipo, conversa.Id, request.Data, request.Texto);

            await _mensagemRepository.Salvar(mensagem);

            return Unit.Value;
        }

    }
}
