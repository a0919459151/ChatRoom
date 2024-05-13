using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Contracts.Auth;

public class SignupViewModel
{
    [Display(Name = "Account")]
    [Required]
    public string? Account { get; set; }

    [Display(Name = "Password")]
    [Required]
    public string? Password { get; set; }
}