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
        private readonly IMessageTokenCache _messageTokenCache;

        private ILogger _logger;

        public ReceivedMessageService(IMessageResponseService messageResponseService,
                                      IMessageTokenCache messageTokenCache,
                                      ISendMessageService sendMessageService,
                                      IOptions<BotOptions> botConfig,
                                      ILogger<ReceivedMessageService> logger)
        {
            _logger = logger;
            _messageResponseService = messageResponseService;
            _messageTokenCache = messageTokenCache;
            _sendMessageService = sendMessageService;
            _botOptions = botConfig.Value;
            _handler = new ViberBotWebhookHandler();
            AddHandlers(_handler);
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

        public TextMessage HandleMessage(CallbackEvent message)
        {
            if (_messageTokenCache.Contains(message.MessageToken))
            {
                var e = new EventId(5000, "Already handled message");
                _logger.LogInformation(e, "An event with message token: {0}, has already been handled, so it is discarded", message.MessageToken);
                return null;
            }
            else
            {
                var result = _handler.HandleEvent(message);
                _messageTokenCache.Add(message.MessageToken);
                return result;
            }
        }
    }
}
