namespace QuizSession.API.DbContext;

public class QuizSessionDbContext(
          IQuizSessionRepository quizSession
        , IParticipantRepository participant
    )
{
    public readonly IQuizSessionRepository QuizSession = quizSession;
    public readonly IParticipantRepository Participant = participant;
}