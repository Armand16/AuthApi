using AuthApi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/users", async (AppContext context) => {

    var users = await context.Users.ToListAsync();

    return Results.Ok(users);
});

app.MapPost("/auth/token", async (AppContext context, [FromBody]AuthTokenRequest request) => {
    // obtenemos al usuario por email y password
    var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);


    return Results.Ok(user);
});

app.Run();
