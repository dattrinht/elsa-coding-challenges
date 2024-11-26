namespace QuizSession.API.Events;

public record UserLeft(long QuizSessionId, long ParticipantId, string UserName) : IQuizSessionMessage;