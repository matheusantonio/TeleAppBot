using MediatR;
using TeleAppBot.Domain.Events;
using TeleAppBot.Domain.Mensageria;

namespace TeleAppBot.Application.Mensagens.EnviarMensagem
{
    public class EnviarMensagemCommandHandler : IRequestHandler<EnviarMensagemCommand>
    {
        private readonly IKafkaService _kafkaService;

        public EnviarMensagemCommandHandler(IKafkaService kafkaService)
        {
            _kafkaService = kafkaService;
        }

        public async Task<Unit> Handle(EnviarMensagemCommand request, CancellationToken cancellationToken)
        {
            if(request.MensagemTexto is not null)
            {
                await _kafkaService.EnviarMensagem(
                    new EnviarMensagemTextoEvent(
                        request.IdMensagem, request.IdChat, request.IdContato, request.Tipo, request.Data, request.MensagemTexto.Texto));
            }

            else if(request.MensagemMidia is not null)
            {
                await _kafkaService.EnviarMensagem(
                    new EnviarMensagemMidiaEvent(
                        request.IdMensagem, request.IdChat, request.IdContato, request.Tipo, request.Data, request.MensagemMidia.IdArquivo, request.MensagemMidia.IdUnicoArquivo, request.MensagemMidia.Tamanho));
            }

            return Unit.Value;
        }

    }
}
