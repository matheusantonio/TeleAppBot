using MediatR;
using TeleAppBot.Domain.Repositories;

namespace TeleAppBot.Application.Conversas.ObterConversa
{
    public class ObterConversaQueryHandler : IRequestHandler<ObterConversaQuery, ObterConversaResponseModel>
    {
        private readonly IConversasRepository _conversasRepository;

        public ObterConversaQueryHandler(IConversasRepository conversasRepository)
        {
            _conversasRepository = conversasRepository;
        }

        public async Task<ObterConversaResponseModel> Handle(ObterConversaQuery request, CancellationToken cancellationToken)
        {
            var conversa = await _conversasRepository.ObterPorIdChat(request.IdChat);

            if (conversa == null)
            {
                return null;
            }

            return new ObterConversaResponseModel(conversa.IdChat, conversa.Id, conversa.Invertida);
        }
    }
}
