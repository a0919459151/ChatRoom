using Microsoft.AspNetCore.SignalR;
using System.Web;

namespace ChatRoom.Hubs;

public class ChatRoomHub : Hub<IChatRoomClient>
{
    // JoinRoom
    public async Task JoinRoom(string userName)
    {
        // Sanitize user input
        userName = HttpUtility.HtmlEncode(userName);

        var message = $"{userName} has joined the room";

        await Clients.Others.JoinRoom(message);
    }

    // LeaveRoom
    public async Task LeaveRoom(string userName)
    {
        // Sanitize user input
        userName = HttpUtility.HtmlEncode(userName);

        var message = $"{userName} has left the room";

        await Clients.Others.LeaveRoom(message);
    }

    // SendMessage
    public async Task SendMessage(string userName, string message)
    {
        // Sanitize user input
        userName = HttpUtility.HtmlEncode(userName);
        message = HttpUtility.HtmlEncode(message);

        // ReceiveMessage to all clients, besides the caller
        await Clients.Others.ReceiveMessage(userName, message);
        
        // ReceiveMessage to the caller
        await Clients.Caller.ReceiveMessageToCaller(message);
    }
}
