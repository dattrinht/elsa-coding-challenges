namespace NotificationServer.Events;

public record SessionStarted(long QuizSessionId) : IQuizSessionMessage;