namespace QuizSession.API.DbContext.Entities;

public class Participant : Entity
{
    public required long QuizSessionId { get; set; }
    public required string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
}