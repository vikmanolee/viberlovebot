using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using viberlovebot.Abstractions;
using viberlovebot.Enumerations;
using viberlovebot.Models;
using ViberBotApi;
using ViberBotApi.Models;
using ViberBotApi.Models.Received;

namespace viberlovebot.Services
{
    public class MessageResponseService : IMessageResponseService
    {
        private readonly ISendMessageService _sendMessageService;
        private readonly BotOptions _botConfig;
        private readonly ViberBotWebhookHandler _handler;

        private ILogger _logger;

        public MessageResponseService(ISendMessageService sendMessageService, IOptions<BotOptions> botConfig, ILogger<ReceivedMessageService> logger)
        {
            _logger = logger;
            _sendMessageService = sendMessageService;
            _botConfig = botConfig.Value;
            _handler = new ViberBotWebhookHandler();
        }

        public IEnumerable<ResponseMessage> CreateResponseFor(MessageReceived message, SenderReceived sender)
        {
            var responses = new List<ResponseMessage>();

            var category = DetermineMessageCategory(message);

            switch (category)
            {
                case MessageCategory.KaliXronia:
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Text, Text = "Καλή χρονιά και χρόνια πολλά! Με υγεία κι ευτυχία το νέο έτος!"});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Sticker, StickerId = 5716});
                    break;
                case MessageCategory.Pasok:
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Sticker, StickerId = 68408});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.AnimatedGif, MediaUrl = "https://media.giphy.com/media/TIRQqWIimnH9xL0kJx/giphy-downsized.gif"});
                    break;
                default:
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Text, Text = $"{sender.Name}, το bot αυτό δεν είναι τόσο εξελιγμένο ακόμα ώστε να καταλαβαίνει τι του λες. Στην επόμενη version θα τα πάμε καλύτερα. Πάρε, όμως, ένα sticker για τώρα."});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Sticker, StickerId = Stickers.Random});
                    break;
            }

            return responses;
        }

        public MessageCategory DetermineMessageCategory(MessageReceived message)
        {
            if (message.Type == MessageTypes.Text)
            {
                if (Regex.IsMatch(message.Text, @"([Κκ]αλ[ηή] [Χχ]ρονι[αά])|([Χχ]ρ[όο]νια [Ππ]ολλ[άα])"))
                {
                    return MessageCategory.KaliXronia;
                }
                if (Regex.IsMatch(message.Text, @"[Ππ]ασ[οό]κ|ΠΑΣΟΚ"))
                {
                    return MessageCategory.Pasok;
                }
            }
            return MessageCategory.Unknown;
        }
    }
}