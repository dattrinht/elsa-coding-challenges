using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<QuizSessionHub>("/hub");
app.MapPost("/test", async ([FromServices] IHubContext<QuizSessionHub> hubContext) =>
{
    // Handshake message
    //{
    //  "protocol": "json",
    //  "version": 1
    //}
    await hubContext.Clients.All.SendAsync("Notify", $"Its worked: {DateTime.Now}");
});

app.Run();