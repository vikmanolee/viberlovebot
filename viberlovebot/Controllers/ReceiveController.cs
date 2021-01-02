using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ViberBotApi.Models.Sent;
using ViberBotApi.Models.Received;
using viberlovebot.Services;

namespace viberlovebot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiveController : ControllerBase
    {
        private readonly IReceivedMessageService _receivedMessageService;
        private readonly ILogger<ReceiveController> _logger;

        public ReceiveController(IReceivedMessageService receivedMessageService, ILogger<ReceiveController> logger)
        {
            _receivedMessageService = receivedMessageService;
            _logger = logger;
        }

        [HttpPost]
        public TextMessage Post([FromBody] CallbackEvent message)
        {
            var userId = message.Sender != null ? message.Sender.Id : message.User != null ? message.User.Id : message.UserId;
            _logger.LogInformation($"User ID: {userId}");
            return _receivedMessageService.HandleMessage(message);
        }

        [HttpGet]
        public TextMessage Get()
        {
            return new TextMessage();
        }
    }
}
