using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViberBotApi;
using ViberBotApi.Models;
using Sent = ViberBotApi.Models.Sent;
using Received = ViberBotApi.Models.Received;

namespace viberlovebot.Services
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly ISendMessageService _sendMessageService;
        private readonly BotConfiguration _botConfig;
        private readonly ViberBotWebhookHandler _handler;

        private ILogger _logger;

        public ReceivedMessageService(ISendMessageService sendMessageService, IOptions<BotConfiguration> botConfig, ILogger<ReceivedMessageService> logger)
        {
            _logger = logger;
            _sendMessageService = sendMessageService;
            _botConfig = botConfig.Value;
            _handler = new ViberBotWebhookHandler();
            AddHandlers(_handler);
        }

        public Sent.TextMessage HandleMessage(Received.CallbackEvent message)
        {
            return _handler.HandleEvent(message);
        }

        private void AddHandlers(ViberBotWebhookHandler handler)
        {
            _handler.conversationStartedHandler = HandleConversationStartedEvent;
            _handler.receivedMessageHandler = HandleMessageEvent;
        }

        public Sent.TextMessage HandleConversationStartedEvent(Received.CallbackEvent message)
        {
            return new Sent.TextMessage
            {
                Text = _botConfig.WelcomeMessage,
                Sender = new Sent.Sender { Name = _botConfig.Sender.Name },
                TrackingData = "tracking welcome message"
            };
        }

        public void HandleMessageEvent(Received.CallbackEvent message)
        {
            var receiver = (Received.Sender)message.Sender;
            var answer = AnswerText(message.Message);
            int sticker = 5716;
            if (string.IsNullOrEmpty(answer))
            {
                answer = $"{receiver.Name}, το bot αυτό δεν είναι τόσο εξελιγμένο ακόμα ώστε να καταλαβαίνει τι του λες. Στην επόμενη version θα τα πάμε καλύτερα. Πάρε, όμως, ένα sticker για τώρα.";
                sticker = Stickers.Random;
            }
            _sendMessageService.TextMessage(receiver.Id, answer);
            _sendMessageService.StickerMessage(receiver.Id, sticker);
        }

        private string AnswerText(Received.MessageReceived message)
        {
            if (message.Type == MessageTypes.Text)
            {
                if (Regex.IsMatch(message.Text, @"([Κκ]αλ[ηή] [Χχ]ρονι[αά])|([Χχ]ρ[όο]νια [Ππ]ολλ[άα])"))
                    return "Καλή χρονιά και χρόνια πολλά! Με υγεία κι ευτυχία το νέο έτος!";
            }
            return "";
        }
    }
}
