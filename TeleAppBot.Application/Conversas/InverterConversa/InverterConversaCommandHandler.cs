using MediatR;
using TeleAppBot.Domain.Repositories;

namespace TeleAppBot.Application.Conversas.InverterConversa
{
    public class InverterConversaCommandHandler : IRequestHandler<InverterConversaCommand, Unit>
    {
        private readonly IConversasRepository _repository;

        public InverterConversaCommandHandler(IConversasRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(InverterConversaCommand request, CancellationToken cancellationToken)
        {
            var conversa = await _repository.ObterPorIdChat(request.IdChat);

            if(conversa is null)
            {
                return Unit.Value;
            }

            if (request.Inverter)
                conversa.Inverter();
            else
                conversa.Reverter();

            await _repository.Atualizar(conversa);

            return Unit.Value;
        }
    }
}
