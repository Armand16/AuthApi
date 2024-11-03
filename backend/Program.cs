using AuthApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => {

    List<User> users = [
        new(){ Id = 1, Name = "Armando" },
        new(){ Id = 2, Name = "Javier" },
    ];

    return Results.Ok(users);
});

app.Run();
