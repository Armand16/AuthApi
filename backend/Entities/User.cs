using System.Text.Json.Serialization;

namespace AuthApi.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore]
    public List<UserRole> UserRoles { get; set; } = [];
    public required string Email { get; set; }
    public required string Password { get; set; }
}
