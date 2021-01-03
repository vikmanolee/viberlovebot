using System.Threading.Tasks;
using ViberBotApi.Models.Sent;

namespace ViberBotApi.Abstractions
{
    public interface IViberBot
    {
        Task<SendMessageResponse> SendTextMessage(TextMessage message);
        Task<SendMessageResponse> SendStickerMessage(StickerMessage message);
        Task<SendMessageResponse> SendUrlMessage(UrlMessage message);
    }
}