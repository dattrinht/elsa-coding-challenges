namespace NotificationServer.Events;

public record UserSubmitted(long QuizSessionId, long ParticipantId, long QuestionId, long AnswerId, bool IsCorrect) : IQuizSessionMessage;