using Flurl;
using Microsoft.Extensions.Configuration;
using viberlovebot.Abstractions;

namespace viberlovebot.Services
{
    public class MediaUrlResolver : IMediaUrlResolver
    {
        private const string menuImagesFolder = "images/menu";
        private string _baseUrl;

        public MediaUrlResolver(IConfiguration configuration)
        {
            _baseUrl = configuration.GetValue<string>("MediaBaseUrl");
        }

        public string GetMenuImageFor(string firm, string item)
        {
            return Url.Combine(_baseUrl, menuImagesFolder, firm, $"{item}.jpg");
        }
    }
}