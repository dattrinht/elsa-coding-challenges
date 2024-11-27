namespace NotificationServer.Consumers;

public class SessionStartedConsumer(IHubContext<QuizSessionHub> hubContext) : IConsumer<SessionStarted>
{
    private readonly IHubContext<QuizSessionHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<SessionStarted> context)
    {
        await _hubContext.Clients.All.SendAsync("SessionStarted", JsonSerializer.Serialize(context.Message));
    }
}