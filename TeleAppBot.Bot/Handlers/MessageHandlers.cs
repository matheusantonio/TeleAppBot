using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeleAppBot.Bot.Handlers
{
    public class MessageHandlers
    {
        private readonly ITelegramBotClient _botClient;

        private bool invert = false;

        public MessageHandlers(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleMessage(Message message)
        {
            var user = message.From;
            var text = message.Text ?? string.Empty;

            if (user is null)
                return;

            if (text.StartsWith("/"))
                await HandleCommand(user.Id, text);
            else
            {
                if (invert)
                    await _botClient.SendTextMessageAsync(user.Id, new string(text.Reverse().ToArray()));
                else
                    await _botClient.CopyMessageAsync(user.Id, user.Id, message.MessageId);
            }

        }

        public async Task HandleCommand(long userId, string text)
        {
            switch (text)
            {
                case "/invert":
                    invert = !invert;
                    if (invert)
                        await _botClient.SendTextMessageAsync(userId, new string("Bip bop! Now all messages are inverted!".Reverse().ToArray()));
                    else
                        await _botClient.SendTextMessageAsync(userId, "Bip bop! Now all messages are back to normal!");
                    break;
                default:
                    await _botClient.SendTextMessageAsync(userId, "Bip bop! Command not recognized!");
                    break;
            }
        }
    }
}
