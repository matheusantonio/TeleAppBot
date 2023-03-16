using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

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
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(mensagem), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {

            }
        }

        public async Task InverterConversa(long idChat, bool inverter)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.Url}/conversa")
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new InverterConversaRequest(idChat, inverter)), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {

            }
        }

        public async Task<ObterConversaResponse> ObterConversa(long idChat)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.Url}/conversa/{idChat}");

            var response = await _client.SendAsync(request);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ObterConversaResponse>(jsonResult);

            return result;
        }
    }
}
