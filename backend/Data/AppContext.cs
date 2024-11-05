using AuthApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}
