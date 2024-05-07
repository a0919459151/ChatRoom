using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Hubs;

public class ChatRoomHub : Hub<IChatRoomClient>
{
    // JoinRoom
    public async Task JoinRoom(string userName)
    {
        var message = $"{userName} has joined the room";

        await Clients.Others.JoinRoom(message);
    }

    // LeaveRoom
    public async Task LeaveRoom(string userName)
    {
        var message = $"{userName} has left the room";

        await Clients.Others.LeaveRoom(message);
    }

    // SendMessage
    public async Task SendMessage(string userName, string message)
    {
        // ReceiveMessage to all clients, besides the caller
        await Clients.Others.ReceiveMessage(userName, message);
        
        // ReceiveMessage to the caller
        await Clients.Caller.ReceiveMessageToCaller(message);
    }
}
