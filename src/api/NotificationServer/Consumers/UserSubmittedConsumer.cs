namespace NotificationServer.Consumers;

public class UserSubmittedConsumer(IHubContext<QuizSessionHub> hubContext) : IConsumer<UserSubmitted>
{
    private readonly IHubContext<QuizSessionHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<UserSubmitted> context)
    {
        await _hubContext.Clients.All.SendAsync("UserSubmitted", JsonSerializer.Serialize(context.Message));
    }
}