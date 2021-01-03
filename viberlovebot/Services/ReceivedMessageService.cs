using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using viberlovebot.Abstractions;
using viberlovebot.Enumerations;
using viberlovebot.Models;
using ViberBotApi;
using ViberBotApi.Models;
using ViberBotApi.Models.Sent;
using ViberBotApi.Models.Received;

namespace viberlovebot.Services
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly IMessageResponseService _messageResponseService;
        private readonly ISendMessageService _sendMessageService;
        private readonly BotConfiguration _botConfig;
        private readonly ViberBotWebhookHandler _handler;

        private ILogger _logger;

        public ReceivedMessageService(IMessageResponseService messageResponseService,
                                      ISendMessageService sendMessageService,
                                      IOptions<BotConfiguration> botConfig,
                                      ILogger<ReceivedMessageService> logger)
        {
            _logger = logger;
            _messageResponseService = messageResponseService;
            _sendMessageService = sendMessageService;
            _botConfig = botConfig.Value;
            _handler = new ViberBotWebhookHandler();
            AddHandlers(_handler);
        }

        public TextMessage HandleMessage(CallbackEvent message)
        {
            return _handler.HandleEvent(message);
        }

        private void AddHandlers(ViberBotWebhookHandler handler)
        {
            _handler.conversationStartedHandler = HandleConversationStartedEvent;
            _handler.receivedMessageHandler = HandleMessageEvent;
        }

        public TextMessage HandleConversationStartedEvent(CallbackEvent @event)
        {
            return new TextMessage
            {
                Text = _botConfig.WelcomeMessage,
                Sender = new Sender { Name = _botConfig.Sender.Name },
                TrackingData = "tracking welcome message"
            };
        }

        public void HandleMessageEvent(CallbackEvent @event)
        {
            if (@event.Type == CallbackEventTypes.Message)
            {
                var responses = _messageResponseService.CreateResponseFor(@event.Message, @event.Sender);
                foreach (var response in responses)
                {
                    SendMessage(response, @event.Sender.Id);
                }
            }
        }

        private void SendMessage(ResponseMessage response, string receiverId)
        {
            switch (response.Type)
            {
                case ResponseMessageType.Text:
                    _sendMessageService.TextMessage(receiverId, response.Text);
                    break;
                case ResponseMessageType.Sticker:
                    _sendMessageService.StickerMessage(receiverId, response.StickerId);
                    break;
                default:
                    break;
            }
        }
    }
}
