namespace AuthApi.Entities;

public class UserRole
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required int RoleId { get; set; }
    public required Role Role { get; set; }
    public required User User { get; set; }
}
