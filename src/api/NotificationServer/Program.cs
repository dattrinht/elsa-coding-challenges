var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();

    x.AddRider(rider =>
    {
        rider.AddConsumer<SessionStartedConsumer>();
        rider.AddConsumer<UserJoinedConsumer>();
        rider.AddConsumer<UserLeftConsumer>();
        rider.AddConsumer<UserSubmittedConsumer>();

        rider.UsingKafka((context, k) =>
        {
            k.Host("localhost:9092");

            k.TopicEndpoint<SessionStarted>("quiz.session.sessionStarted.v1", "notification-server", e =>
            {
                e.ConfigureConsumer<SessionStartedConsumer>(context);
            });

            k.TopicEndpoint<UserJoined>("quiz.session.userJoined.v1", "notification-server", e =>
            {
                e.ConfigureConsumer<UserJoinedConsumer>(context);
            });

            k.TopicEndpoint<UserLeft>("quiz.session.userLeft.v1", "notification-server", e =>
            {
                e.ConfigureConsumer<UserLeftConsumer>(context);
            });

            k.TopicEndpoint<UserSubmitted>("quiz.session.userSubmitted.v1", "notification-server", e =>
            {
                e.ConfigureConsumer<UserSubmittedConsumer>(context);
            });
        });
    });
});

var app = builder.Build();

app.MapHub<QuizSessionHub>("/hub");
app.MapPost("/hello", async ([FromServices] IHubContext<QuizSessionHub> hubContext) =>
{
    // Handshake message
    //{
    //  "protocol": "json",
    //  "version": 1
    //}
    await hubContext.Clients.All.SendAsync("Notify", $"Its worked: {DateTime.Now}");
});

app.Run();