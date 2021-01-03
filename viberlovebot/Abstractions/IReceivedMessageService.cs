using ViberBotApi.Models.Received;
using ViberBotApi.Models.Sent;

namespace viberlovebot.Abstractions
{
    public interface IReceivedMessageService
    {
        TextMessage HandleMessage(CallbackEvent message);
    }
}
