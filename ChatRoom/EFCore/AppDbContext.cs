using ChatRoom.EFCore.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.EFCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
