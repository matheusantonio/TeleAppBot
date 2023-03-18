using MediatR;

namespace TeleAppBot.Application.Conversas.InverterConversa
{
    public record InverterConversaCommand(long IdChat, bool Inverter) : IRequest<Unit>;
}
