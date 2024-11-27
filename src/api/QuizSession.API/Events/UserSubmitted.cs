namespace QuizSession.API.Events;

public record UserSubmitted(long QuizSessionId, long ParticipantId, bool IsCorrect) : IQuizSessionMessage;