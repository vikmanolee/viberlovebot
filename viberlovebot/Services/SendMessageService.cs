using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using viberlovebot.Abstractions;
using viberlovebot.Enumerations;
using viberlovebot.Models;
using ViberBotApi;
using ViberBotApi.Models.Sent;
using ViberBotApi.Configuration;

namespace viberlovebot.Services
{
    public class SendMessageService : ISendMessageService
    {
        private readonly BotOptions _botOptions;
        private ILogger _logger;
        private ViberBot _api;

        public SendMessageService(IOptions<ViberBotApiConfiguration> viberConfig, IOptions<BotOptions> botConfig, ILogger<SendMessageService> logger)
        {
            _logger = logger;
            _botOptions = botConfig.Value;
            _api = new ViberBot(viberConfig.Value);
        }

        public void SendResponse(ResponseMessage response, string receiverId)
        {
            switch (response.Type)
            {
                case ResponseMessageType.Text:
                    TextMessage(receiverId, response.Text);
                    break;
                case ResponseMessageType.Sticker:
                    StickerMessage(receiverId, response.StickerId);
                    break;
                case ResponseMessageType.AnimatedGif:
                    UrlMessage(receiverId, response.MediaUrl);
                    break;
                default:
                    break;
            }
        }

        public async void TextMessage(string receiver, string text)
        {
            var message = new TextMessage {
                Receiver = receiver,
                Text = text,
                Sender = new Sender { Name = _botOptions.Sender.Name }
            };
            var response = await _api.SendTextMessage(message);
            HandleResponse(response);
        }

        public async void StickerMessage(string receiver, int stickerId)
        {
            var message = new StickerMessage {
                Receiver = receiver,
                StickerId = stickerId,
                Sender = new Sender { Name = _botOptions.Sender.Name }
            };
            var response = await _api.SendStickerMessage(message);            
            HandleResponse(response);
        }

        public async void UrlMessage(string receiver, string url)
        {
            var message = new UrlMessage {
                Receiver = receiver,
                Media = url,
                Sender = new Sender { Name = _botOptions.Sender.Name }
            };
            var response = await _api.SendUrlMessage(message);            
            HandleResponse(response);
        }

        private void HandleResponse(SendMessageResponse response)
        {
            if (response.Success == false)
                _logger.LogError(response.ErrorMessage);
            if (response.Status != 0)
                _logger.LogError(response.StatusMessage);
        }
    }
}
