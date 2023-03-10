using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeleAppBot.Bot.Handlers;
using Telegram.Bot;

namespace TeleAppBot.Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            StartBot(serviceProvider);
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var botKey = config["TELEGRAM_BOT_TOKEN"];

            services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botKey!));

            services.AddSingleton<UpdateHandler>();
            services.AddSingleton<ErrorHandlers>();

            services.AddScoped<MessageHandlers>();
        }

        private static void StartBot(IServiceProvider serviceProvider)
        {
            using var cts = new CancellationTokenSource();

            var updateHandler = serviceProvider.GetRequiredService<UpdateHandler>();
            var errorHandler = serviceProvider.GetRequiredService<ErrorHandlers>();

            var bot = serviceProvider.GetRequiredService<ITelegramBotClient>();

            bot.StartReceiving(
                updateHandler: updateHandler.HandleUpdate,
                pollingErrorHandler: errorHandler.HandleError,
                cancellationToken: cts.Token);

            Console.WriteLine("TeleAppBot Started!");
            Console.ReadLine();

            cts.Cancel();
        }
    }
}
