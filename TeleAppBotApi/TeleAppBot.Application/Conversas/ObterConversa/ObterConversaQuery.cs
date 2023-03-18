using MediatR;

namespace TeleAppBot.Application.Conversas.ObterConversa
{
    public record ObterConversaQuery(long IdChat) : IRequest<ObterConversaResponseModel>;
}
