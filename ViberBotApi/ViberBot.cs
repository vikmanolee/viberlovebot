using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using ViberBotApi.Abstractions;
using ViberBotApi.Configuration;
using ViberBotApi.Models.Sent;

namespace ViberBotApi
{
    public class ViberBot : IViberBot
    {
        private HttpClient _client;
        private ViberBotApiConfiguration _configuration;

        public ViberBot(ViberBotApiConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient();
            _client.AddAuthHeader(_configuration.AuthToken);
        }

        public async Task<SendMessageResponse> SendMessage<TMessage>(TMessage message)
            where TMessage : Message
        {
            var response = await PostJsonHttpClient(UrlResources.SendMessage(), message);
            return await HandleResponse(response);
        }

        private async Task<HttpResponseMessage> PostJsonHttpClient<TMessage>(string uri, TMessage message)
        {
            return await _client.PostAsJsonAsync(uri, message);
        }

        private async Task<SendMessageResponse> HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<SendMessageResponse>();
                }
                catch (System.Exception)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new SendMessageResponse { Success = true, ErrorMessage = errorContent };
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new SendMessageResponse { Success = false, ErrorMessage = errorContent };
            }
        }
    }
}
