using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Social.API.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private static readonly ConcurrentQueue<string> _receivedMessages = new();

        [HttpGet]
        public IActionResult GetMessages()
        {
            return Ok(_receivedMessages.ToArray());
        }

        public static void AddMessage(string message)
        {
            _receivedMessages.Enqueue(message);
            if (_receivedMessages.Count > 10)
            {
                _receivedMessages.TryDequeue(out _);
            }
        }
    }
}
