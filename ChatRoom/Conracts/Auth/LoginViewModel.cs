using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Contracts.Auth;

public class LoginViewModel
{
    // Nick Name
    [Display(Name = "Nick Name")]
    [Required]
    public string? NickName { get; set; }
}
