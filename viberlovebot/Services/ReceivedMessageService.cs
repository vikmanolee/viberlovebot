using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using viberlovebot.Abstractions;
using ViberBotApi;
using ViberBotApi.Models.Sent;
using ViberBotApi.Models.Received;

namespace viberlovebot.Services
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly IMessageResponseService _messageResponseService;
        private readonly ISendMessageService _sendMessageService;
        private readonly BotOptions _botOptions;
        private readonly ViberBotWebhookHandler _handler;

        private ILogger _logger;

        public ReceivedMessageService(IMessageResponseService messageResponseService,
                                      ISendMessageService sendMessageService,
                                      IOptions<BotOptions> botConfig,
                                      ILogger<ReceivedMessageService> logger)
        {
            _logger = logger;
            _messageResponseService = messageResponseService;
            _sendMessageService = sendMessageService;
            _botOptions = botConfig.Value;
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
                Text = _botOptions.WelcomeMessage,
                Sender = new Sender { Name = _botOptions.Sender.Name },
                TrackingData = "tracking welcome message"
            };
        }

        public void HandleMessageEvent(CallbackEvent @event)
        {
            var responses = _messageResponseService.CreateResponseFor(@event.Message, @event.Sender);
            foreach (var response in responses)
            {
                _sendMessageService.SendResponse(response, @event.Sender.Id);
            }
        }
    }
}
