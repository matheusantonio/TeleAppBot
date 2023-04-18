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
            Console.WriteLine($"Api URL: {_config.Url} {JsonSerializer.Serialize(mensagem)}");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.Url}/mensagem")
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(mensagem), Encoding.UTF8, "application/json")
            };

            try
            {
                var response = await _client.SendAsync(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var stringContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"{response.StatusCode} {stringContent}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro ao consultar serviço TeleAppApi: {ex.Message} {ex.InnerException?.Message} {ex.InnerException?.InnerException?.Message}");
            }
        }

        public async Task InverterConversa(long idChat, bool inverter)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_config.Url}/conversa")
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new InverterConversaRequest(idChat, inverter)), Encoding.UTF8, "application/json")
            };

            try
            {
                var response = await _client.SendAsync(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar serviço TeleAppApi: {ex.Message} {ex.InnerException}");
            }
        }

        public async Task<ObterConversaResponse> ObterConversa(long idChat)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.Url}/conversa/{idChat}");

            try
            {
                var response = await _client.SendAsync(request);

                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ObterConversaResponse>(jsonResult);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao consultar serviço TeleAppApi: {ex.Message} {ex.InnerException}");
            }

            return default;
        }
    }
}
