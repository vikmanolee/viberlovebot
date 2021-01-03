using System.Collections.Generic;
using ViberBotApi.Models.Received;
using viberlovebot.Models;

namespace viberlovebot.Abstractions
{
    public interface IMessageResponseService
    {
        IEnumerable<ResponseMessage> CreateResponseFor(MessageReceived message, SenderReceived sender);
    }
}
