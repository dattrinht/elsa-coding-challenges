namespace NotificationServer.Consumers;

public class UserJoinedConsumer(IHubContext<QuizSessionHub> hubContext) : IConsumer<UserJoined>
{
    private readonly IHubContext<QuizSessionHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<UserJoined> context)
    {
        await _hubContext.Clients.All.SendAsync("UserJoined", JsonSerializer.Serialize(context.Message));
    }
}