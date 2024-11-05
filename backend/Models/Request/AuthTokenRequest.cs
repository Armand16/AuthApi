namespace AuthApi.Models.Request;

public class AuthTokenRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
