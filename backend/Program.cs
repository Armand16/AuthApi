using AuthApi.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/users", async (AppContext context) => {

    var users = await context.Users.ToListAsync();

    return Results.Ok(users);
});

app.Run();
