using Microsoft.EntityFrameworkCore;

namespace ChatRoom.EFCore.DBEntities;

[Comment("User")]
public class User
{
    // Id
    [Comment("Account")]
    public int Id { get; set; }

    // Account
    [Comment("Account")]
    public required string Account { get; set; }

    // Password
    [Comment("Password")]
    public required string Password { get; set; }

    // Nick Name
    [Comment("Nick Name")]
    public string? NickName { get; set; }
}
