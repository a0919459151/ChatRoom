namespace ChatRoom.Hubs;

public interface IChatRoomClient
{
    Task JoinRoom(string message);
    Task LeaveRoom(string message);
    Task ReceiveMessage(string userName, string message);
    Task ReceiveMessageToCaller(string message);
}
