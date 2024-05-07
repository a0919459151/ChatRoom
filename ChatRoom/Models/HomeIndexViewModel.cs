using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Models;

public class HomeIndexViewModel(string userName)
{
    [Display(Name = "User Name")]
    public required string UserName { get; set; } = userName;
}
