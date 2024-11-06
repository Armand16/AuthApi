namespace AuthApi.Models.Requests;

public class AuthTokenRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
