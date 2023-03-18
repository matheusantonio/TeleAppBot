using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeleAppBot.Bot.ExternalServices;
using TeleAppBot.Bot.Handlers;
using Telegram.Bot;

namespace TeleAppBot.Bot.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var botKey = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");

            services.Configure<TeleAppBotApiConfig>(configuration.GetSection("TeleAppBotApi"));

            services.AddHttpClient<TeleAppBotService>();

            services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botKey!));

            services.AddSingleton<UpdateHandler>();
            services.AddSingleton<ErrorHandlers>();

            services.AddScoped<MessageHandlers>();
        }
    }
}
