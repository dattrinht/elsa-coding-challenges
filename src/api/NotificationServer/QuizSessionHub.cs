namespace NotificationServer;

public class QuizSessionHub : Hub
{
    public async Task SendMessage(string content)
        => await Clients.All.SendAsync("ReceiveMessage", content);
}