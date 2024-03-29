﻿using Telegram.Bot;

namespace TeleAppBot.Bot.Handlers
{
    public class ErrorHandlers
    {
        public async Task HandleError(ITelegramBotClient _, Exception exception, CancellationToken cancellationToken)
            => await Console.Error.WriteLineAsync(exception.Message);
    }

}
