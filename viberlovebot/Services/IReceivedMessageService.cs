using ViberBotApi.Models.Received;
using ViberBotApi.Models.Sent;

namespace viberlovebot.Services
{
    public interface IReceivedMessageService
    {
        TextMessage HandleMessage(CallbackEvent message);
    }
}
