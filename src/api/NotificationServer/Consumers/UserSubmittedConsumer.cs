namespace NotificationServer.Consumers;

public static class LeaderboardStore
{
    public static Dictionary<string, Dictionary<string, Int16>> Data = [];
}

public class UserSubmittedConsumer(IHubContext<QuizSessionHub> hubContext) : IConsumer<UserSubmitted>
{
    private readonly IHubContext<QuizSessionHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<UserSubmitted> context)
    {
        var msg = context.Message;
        if (!msg.IsCorrect)
        {
            return;
        }

        var leaderboardCacheKey = $"{msg.QuizSessionId}";
        var isLeaderboardExisted = LeaderboardStore.Data.TryGetValue(leaderboardCacheKey, out var currentLeaderboard);
        if (!isLeaderboardExisted)
        {
            currentLeaderboard = [];
            LeaderboardStore.Data.TryAdd(leaderboardCacheKey, currentLeaderboard);
        }

        var userScoreCacheKey = $"{msg.QuizSessionId}__{msg.ParticipantId}";
        var isUserScoreExisted = currentLeaderboard!.TryGetValue(userScoreCacheKey, out var score);
        if (!isUserScoreExisted)
        {
            currentLeaderboard.TryAdd(userScoreCacheKey, 1);
            await _hubContext.Clients.All.SendAsync("UserScoreUpdated", JsonSerializer.Serialize(new
            {
                msg.QuizSessionId,
                msg.ParticipantId,
                Score = 1,
            }));
        } 
        else
        {
            currentLeaderboard[userScoreCacheKey] = ++score;
            await _hubContext.Clients.All.SendAsync("UserScoreUpdated", JsonSerializer.Serialize(new
            {
                msg.QuizSessionId,
                msg.ParticipantId,
                Score = score,
            }));
        }

        await _hubContext.Clients.All.SendAsync("LeaderboardUpdated", JsonSerializer.Serialize(new
        {
            msg.QuizSessionId,
            Leaderboard = currentLeaderboard,
        }));
    }
}