using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Contracts.ChatRoom;

public class ChatRoomViewModel
{
    [Display(Name = "User Name")]
    public string? UserName { get; set; }
}
