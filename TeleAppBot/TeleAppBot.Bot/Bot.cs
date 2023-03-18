using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeleAppBot.Bot.Handlers;
using Telegram.Bot;

namespace TeleAppBot.Bot
{
    public class Bot : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public Bot(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceFactory.CreateScope();

            var updateHandler = scope.ServiceProvider.GetRequiredService<UpdateHandler>();
            var errorHandler = scope.ServiceProvider.GetRequiredService<ErrorHandlers>();

            var bot = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            bot.StartReceiving(
                updateHandler: updateHandler.HandleUpdate,
                pollingErrorHandler: errorHandler.HandleError,
                cancellationToken: stoppingToken);

            Console.WriteLine("TeleAppBot Started!");

            while (!stoppingToken.IsCancellationRequested) { }

            return Task.CompletedTask;
        }
    }
}
