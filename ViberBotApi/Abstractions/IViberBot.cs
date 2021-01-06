using System.Threading.Tasks;
using ViberBotApi.Models.Sent;

namespace ViberBotApi.Abstractions
{
    public interface IViberBot
    {
        Task<SendMessageResponse> SendMessage<TMessage>(TMessage message)
            where TMessage : Message;
    }
}