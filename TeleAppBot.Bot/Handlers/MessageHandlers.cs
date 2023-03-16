using TeleAppBot.Bot.ExternalServices;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TeleAppBot.Bot.Handlers
{
    public class MessageHandlers
    {
        private readonly ITelegramBotClient _botClient;
        private readonly TeleAppBotService _botService;

        private bool invert = false;

        public MessageHandlers(ITelegramBotClient botClient, TeleAppBotService botService)
        {
            _botClient = botClient;
            _botService = botService;
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
                var request = new EnviarMensagemRequest(
                    message.MessageId, message.Chat.Id, user.Id,
                    TipoMensagem.Texto, message.Date,
                    new InformacoesContatoRequest(user.IsBot, user.FirstName, user.LastName, user.Username),
                    new MensagemTextoRequest(text), null);

                await _botService.EnviarMensagem(request);

                var responseMessage = text;
                if (invert)
                {
                    responseMessage = new string(text.Reverse().ToArray());
                    await _botClient.SendTextMessageAsync(user.Id, responseMessage);
                }
                else
                    await _botClient.CopyMessageAsync(user.Id, user.Id, message.MessageId);

                request = request with { MensagemTexto = new MensagemTextoRequest(responseMessage) };

                await _botService.EnviarMensagem(request);
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
