namespace QuizSession.API.DbContext.Entities;

public class QuizSession : Entity
{
    public required string Status { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public static class QuizSessionStatus
{
    public static string Open => "open";
    public static string Started => "started";
    public static string Closed => "closed";
}