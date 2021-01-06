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
        private readonly IMediaUrlResolver _mediaUrlResolver;
        private readonly BotOptions _botConfig;

        public MessageResponseService(IMediaUrlResolver mediaUrlResolver, IOptions<BotOptions> botConfig)
        {
            _mediaUrlResolver = mediaUrlResolver;
            _botConfig = botConfig.Value;
        }

        public IEnumerable<ResponseMessage> CreateResponseFor(MessageReceived message, SenderReceived sender)
        {
            var responses = new List<ResponseMessage>();

            var category = DetermineMessageCategory(message);

            switch (category)
            {
                case MessageCategory.Love:
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Text, Text = "Κι εγώ αγάπη μου!"});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Sticker, StickerId = 40108});
                    break;
                case MessageCategory.KaliXronia:
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Text, Text = "Καλή χρονιά και χρόνια πολλά! Με υγεία κι ευτυχία το νέο έτος!"});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Sticker, StickerId = 5716});
                    break;
                case MessageCategory.Pasok:
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Sticker, StickerId = 68408});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.AnimatedGif, MediaUrl = "https://media.giphy.com/media/TIRQqWIimnH9xL0kJx/giphy-downsized.gif"});
                    break;
                case MessageCategory.OrderStart:
                    var url = _mediaUrlResolver.GetMenuImageFor("pala", "pala_front");
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Picture, MediaUrl = url, Text = "Θα παραγγείλεις από το Pala"});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Text, Text = "Τι θα προτιμούσες;\npinsa (μια παραλλαγή πίτσας), σάντουιτς ή κάποιο snack;" });
                    break;
                case MessageCategory.OrderPinsa:
                    var url2 = _mediaUrlResolver.GetMenuImageFor("pala", "pinse1");
                    var url3 = _mediaUrlResolver.GetMenuImageFor("pala", "pinse2");
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Picture, MediaUrl = url2, Text = "Διάλεξε μια από αυτές"});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Picture, MediaUrl = url3, Text = "... ή αυτές"});
                    break;
                case MessageCategory.OrderSnack:
                    var url4 = _mediaUrlResolver.GetMenuImageFor("pala", "snacks");
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Picture, MediaUrl = url4, Text = "Διάλεξε κάποιο"});
                    break;
                case MessageCategory.OrderSandwitch:
                    var url5 = _mediaUrlResolver.GetMenuImageFor("pala", "sandwiches");
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Picture, MediaUrl = url5, Text = "Διάλεξε κάποιο"});
                    break;
                default:
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Text, Text = FillPlaceholders(_botConfig.DefaultMessage, sender)});
                    responses.Add(new ResponseMessage{ Type = ResponseMessageType.Sticker, StickerId = Stickers.Random});
                    break;
            }

            return responses;
        }

        public MessageCategory DetermineMessageCategory(MessageReceived message)
        {
            if (message.Type == MessageTypes.Text)
            {
                if (Regex.IsMatch(message.Text, @"[Σσ]['\sε]?\s?αγαπ[αά]?[ωώ]"))
                {
                    return MessageCategory.Love;
                }
                if (Regex.IsMatch(message.Text, @"([Κκ]αλ[ηή] [Χχ]ρονι[αά])|([Χχ]ρ[όο]νια [Ππ]ολλ[άα])"))
                {
                    return MessageCategory.KaliXronia;
                }
                if (Regex.IsMatch(message.Text, @"[Ππ]ασ[οό]κ|ΠΑΣΟΚ"))
                {
                    return MessageCategory.Pasok;
                }
                if (Regex.IsMatch(message.Text, @"[Ππ]αραγγε[ιί]?λ[ίι]?[α|ω]"))
                {
                    return MessageCategory.OrderStart;
                }
                if (Regex.IsMatch(message.Text, @"([Ππ][ιί]τσα)|([Pp]insa)"))
                {
                    return MessageCategory.OrderPinsa;
                }
                if (Regex.IsMatch(message.Text, @"([Ss]nack)|(σν[άα]κ)"))
                {
                    return MessageCategory.OrderSnack;
                }
                if (Regex.IsMatch(message.Text, @"([Σσ][άα]ντουιτ[ςσ])|([Ss]andwitch)"))
                {
                    return MessageCategory.OrderSandwitch;
                }
            }
            return MessageCategory.Unknown;
        }

        public string FillPlaceholders(string text, SenderReceived sender)
        {
            var newText = text;
            newText = text.Replace("<sender_name>", sender.Name);
            return newText;
        }
    }
}