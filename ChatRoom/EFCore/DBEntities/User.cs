namespace ChatRoom.EFCore.DBEntities;

public class User
{
    // Id
    public required Guid Id { get; set; }

    // Nick Name
    public required string NickName { get; set; }
}
