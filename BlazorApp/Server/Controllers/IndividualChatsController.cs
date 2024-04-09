using Microsoft.AspNetCore.Mvc;
using BlazorApp.Shared;
using BlazorApp.Server.Services;
using BlazorApp.Server.Models;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/individualchats")]
    public class IndividualChatsController : ControllerBase
    {
        private readonly IndividualChatService _individualChatService;
        private readonly UserService _userService;
        private readonly Logger _logger;

        public IndividualChatsController(IndividualChatService individualChatService, UserService userService)
        {
            _individualChatService = individualChatService;
            _userService = userService;
            _logger = new Logger();
        }

        [HttpPost("start")]
        [Consumes("application/json")]
        public IActionResult StartIndividualChat([FromBody] StartIndividualChatRequest request)
        {
            // Check if the chat already exists between the two users
            var existingChat = _individualChatService.GetIndividualChatByUsernames(request.User1Username, request.User2Username);

            if (existingChat != null)
            {
                // Chat already exists, return existing chat details
                return Ok(existingChat);
            }
            else
            {
                // Create a new chat if it doesn't exist
                var newChat = new IndividualChat
                {
                    User1Id = (_userService.GetUserByUsername(request.User1Username)).Id,
                    User2Id = (_userService.GetUserByUsername(request.User2Username)).Id,
                };

                _individualChatService.InsertIndividualChat(newChat);

                // Return the newly created chat
                return Ok(newChat);
            }
        }

        // Endpoint to get messages for a specific individual chat by chat ID
        [HttpGet("{chatId}/messages")]
        public IActionResult GetMessagesForIndividualChat(int chatId)
        {
            _logger.Log($"CLIENT: Requesting messages for individual chat with ID {chatId}");
            // Retrieve messages for the individual chat from the service
            var messages = _individualChatService.GetMessagesForIndividualChat(chatId);
            _logger.Log($"SERVER: Sending messages for individual chat with ID {chatId}");
            // Return the messages to the client
            return Ok(messages);
        }

        // Endpoint to send a message to a specific individual chat by chat ID
        [HttpPost("{chatId}/messages")]
        [Consumes("application/json")]
        public IActionResult SendMessageToIndividualChat(int chatId, [FromBody] MessageModel message)
        {
            _logger.Log($"CLIENT: Sending message to individual chat with ID {chatId}");
            // Call the service method to send the message to the individual chat
            _individualChatService.SendMessageToIndividualChat(chatId, message);
            _logger.Log($"SERVER: Message sent to individual chat with ID {chatId}");
            // Return success response to the client
            return Ok();
        }
    }
}
