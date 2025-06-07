using System.Net.WebSockets;
using System.Collections.Concurrent;
using System.Text;

namespace SocialLensApp.Services;
public class WebsocketChatHandler
{
    ConcurrentDictionary<string, ChatRoom> _chatRooms = new ();
    public async Task HandleConnection(string chatId, string userId, WebSocket socket)
    {
        var Room = _chatRooms.GetOrAdd(chatId, _ => new ChatRoom(chatId));
        Room.AddUser(userId, socket);
        var buffer = new byte[1024 * 4];
        try
        {
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await Room.BroadcastMessage(userId, message);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions as needed
        }
        finally
        {
            await Room.RemoveUser(userId);
            if (Room.IsEmpty)
            {
                _chatRooms.TryRemove(chatId, out _);
            }
        }
    }
}

internal class ChatRoom
{
    public string Id { get; }
    private readonly ConcurrentDictionary<string, WebSocket> _users = new();
    public ChatRoom(string id)
    {
        Id = id;
    }

    public void AddUser(string userId, WebSocket socket)
    {
        _users[userId] = socket;
    }

    public async Task RemoveUser(string userId)
    {
        if (_users.TryRemove(userId, out var socket))
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "User disconnected", CancellationToken.None);
            }
        }
    }

    public async Task BroadcastMessage(string FromUserId, string message)
    {
        var msgWithSender = $"{FromUserId}: {message}";
        var data = Encoding.UTF8.GetBytes(msgWithSender);
        foreach (var (_, socket) in _users)
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }

    public bool IsEmpty => _users.IsEmpty;

}
