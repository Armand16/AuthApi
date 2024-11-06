namespace AuthApi.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public required string Value { get; set; }
    public DateTime Expiration { get; set; }
    public required bool IsRevoked { get; set; } = false;

    public User? User { get; set; } = null;
}
