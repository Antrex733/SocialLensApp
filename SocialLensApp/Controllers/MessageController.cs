using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using SocialLensApp.Models;
using SocialLensApp.Services;
using SocialLensApp.Services.Interfaces;


namespace SocialLensApp.Controllers
{
    public class MessageController : ControllerBase
    {
        private readonly WebsocketChatHandler ChatHandler;

        public MessageController(WebsocketChatHandler chatHandler)
        {
            ChatHandler = chatHandler;
        }

        [HttpGet("{chatId}/{userId}")]
        public async Task<IActionResult> Connect(string chatId, string userId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await ChatHandler.HandleConnection(chatId, userId, webSocket);
                return new EmptyResult(); // WebSocket connections do not return a response body
            }
            return BadRequest("WebSocket request expected.");
        }

    }
}
