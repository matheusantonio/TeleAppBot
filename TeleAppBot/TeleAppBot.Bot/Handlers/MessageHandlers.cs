using Newtonsoft.Json;
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
                await HandleCommand(user.Id, message.Chat.Id, text);
            else
            {
                var messageJson = System.Text.Json.JsonSerializer.Serialize(message);
                Console.WriteLine($"Received message: {messageJson}");

                var request = new EnviarMensagemRequest(
                    message.MessageId, message.Chat.Id, user.Id,
                    TipoMensagem.Texto, message.Date,
                    new InformacoesContatoRequest(user.IsBot, user.FirstName, user.LastName, user.Username),
                    null, null);

                if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                {
                    request = request with { MensagemTexto = new MensagemTextoRequest(text) };
                }
                else if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                {
                    request = request with
                    {
                        MensagemMidia = new MensagemMidiaRequest(message.Photo.FirstOrDefault()?.FileId, message.Photo.FirstOrDefault()?.FileUniqueId, message.Photo.FirstOrDefault().FileSize.Value)
                    };
                }
                else if(message.Type == Telegram.Bot.Types.Enums.MessageType.Document)
                {
                    request = request with
                    {
                        MensagemMidia = new MensagemMidiaRequest(message.Document.FileId, message.Document.FileUniqueId, message.Document.FileSize.Value)
                    };
                }

                await _botService.EnviarMensagem(request);

                var conversa = await _botService.ObterConversa(message.Chat.Id);

                if (conversa is null)
                    return;

                invert = conversa.Invertida;

                var responseMessage = text;
                if (invert && message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
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

        public async Task HandleCommand(long userId, long chatId, string text)
        {
            switch (text)
            {
                case "/invert":
                    invert = !invert;
                    await _botService.InverterConversa(chatId, invert);
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
