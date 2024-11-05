using AuthApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => {

    List<User> users = [
        new(){ Id = 1, Name = "Armando" },
        new(){ Id = 2, Name = "Javier" },
    ];

    return Results.Ok(users);
});

app.Run();
