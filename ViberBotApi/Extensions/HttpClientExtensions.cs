using System.Net.Http;

namespace ViberBotApi
{
    public static class HttpClientExtensions
    {    public static void AddAuthHeader(this HttpClient client, string authToken)
        {
            client.DefaultRequestHeaders.Add("X-Viber-Auth-Token", authToken);
        }
    }
}