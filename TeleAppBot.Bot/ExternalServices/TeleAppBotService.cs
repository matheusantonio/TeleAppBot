using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.Extensions.Options;
using System.Text;
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
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.Url}/mensagem")
            {
                Content = new StringContent(JsonSerializer.Serialize(mensagem), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {

            }
        }
    }
}
