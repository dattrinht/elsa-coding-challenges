namespace NotificationServer.Consumers;

public class UserLeftConsumer(IHubContext<QuizSessionHub> hubContext) : IConsumer<UserLeft>
{
    private readonly IHubContext<QuizSessionHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<UserLeft> context)
    {
        await _hubContext.Clients.All.SendAsync("UserLeft", JsonSerializer.Serialize(context.Message));
    }
}