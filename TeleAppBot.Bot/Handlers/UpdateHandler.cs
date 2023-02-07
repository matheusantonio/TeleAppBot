using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TeleAppBot.Bot.Handlers
{
    public class UpdateHandler
    {
        private readonly MessageHandlers _messageHandler;

        public UpdateHandler(MessageHandlers messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public async Task HandleUpdate(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await _messageHandler.HandleMessage(update.Message!);
                    break;
                case UpdateType.CallbackQuery:
                    break;
            }
        }
    }
}
