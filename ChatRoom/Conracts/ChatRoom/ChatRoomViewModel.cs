using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Conracts.ChatRoom;

public class ChatRoomViewModel
{
    [Display(Name = "User Name")]
    public string? UserName { get; set; }
}
