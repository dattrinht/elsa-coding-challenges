namespace QuizSession.API.Events;

public record SessionStarted(long QuizSessionId) : IQuizSessionMessage;