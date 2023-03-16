using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace TeleAppBot.Bot.ExternalServices
{
    public class TeleAppBotService
    {
        private readonly HttpClient _client;
        private readonly TeleAppBotApiConfig _config;

        public TeleAppBotService(HttpClient client, IOptions<TeleAppBotApiConfig> config)
        {
            _client = client;
            _config = config.Value;
        }

        public async Task EnviarMensagem(EnviarMensagemRequest mensagem)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/mensagem")
            {
                Content = new StringContent(JsonSerializer.Serialize(mensagem))
            };

            var response = await _client.SendAsync(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {

            }
        }
    }
}
