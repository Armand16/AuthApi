using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthApi.Entities;
using AuthApi.Models.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<AppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// configurar JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/users", async (AppContext context) => {

    var users = await context.Users.ToListAsync();

    return Results.Ok(users);
}).RequireAuthorization();

app.MapPost("/auth/token", async (AppContext context, [FromBody]AuthTokenRequest request) => {
    // obtenemos al usuario por email y password
    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

    if (user == null) return Results.Unauthorized();

    var jwtSettings = configuration.GetSection("JwtSettings");
    var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);
    var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"]));

    // claims del token de acceso
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, request.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        // TODO: agregar claims de roles
    };

    // crear el token de acceso
    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = expires,
        Issuer = jwtSettings["Issuer"],
        Audience = jwtSettings["Audience"],
        SigningCredentials = credentials
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var accessToken = tokenHandler.CreateToken(tokenDescriptor);

    // crear el refresh token
    var refreshToken = new RefreshToken
    {
        Value = Guid.NewGuid().ToString(),
        UserId = user!.Id,
        Expiration = DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["ExpiresInMiHoursRefreskToken"])),
        IsRevoked = false
    };

    // guardar el refresh token en la base de datos
    await context.RefreshTokens.AddAsync(refreshToken);
    await context.SaveChangesAsync();

    // retornar ambos tokens
    return Results.Ok(new
    {
        AccessToken = tokenHandler.WriteToken(accessToken),
        AccessTokenExpiration = expires,
        RefreshToken = refreshToken.Value,
        RefreshTokenExpiration = refreshToken.Expiration
    });
});

app.Run();
