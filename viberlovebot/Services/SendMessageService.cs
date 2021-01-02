using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViberBotApi;
using ViberBotApi.Models.Sent;
using ViberBotApi.Configuration;

namespace viberlovebot.Services
{
    public class SendMessageService : ISendMessageService
    {
        private ILogger _logger;
        private ViberBot _api;

        public SendMessageService(IOptions<ViberBotApiConfiguration> viberConfig, ILogger<SendMessageService> logger)
        {
            _logger = logger;
            _api = new ViberBot(viberConfig.Value);
        }

        public async void TextMessage(string receiver, string text)
        {
            var message = new TextMessage {
                Receiver = receiver,
                Text = text,
                Sender = new Sender { Name = "Love Bot" }
            };
            var response = await _api.SendTextMessage(message);

            if (!response.Success == false)
                _logger.LogError(response.ErrorMessage);
            if (response.Status != 0)
                _logger.LogError(response.StatusMessage);
        }

        public async void StickerMessage(string receiver, int stickerId)
        {
            var message = new StickerMessage {
                Receiver = receiver,
                StickerId = stickerId,
                Sender = new Sender { Name = "Love Bot" }
            };
            var response = await _api.SendStickerMessage(message);            

            if (response.Success == false)
                _logger.LogError(response.ErrorMessage);
            if (response.Status != 0)
                _logger.LogError(response.StatusMessage);
        }
    }
}
