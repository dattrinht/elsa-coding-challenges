namespace QuizSession.API.Features.SessionPrepare;

public record OpenNewSessionRequest(string User);

public record OpenNewSessionResponse(long QuizSessionId);

public class OpenNewSession(QuizSessionDbContext dbContext) : Endpoint<OpenNewSessionRequest, OpenNewSessionResponse>
{
    private readonly QuizSessionDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/v1/session-prepare/open");
        AllowAnonymous();
        Description(b => b
            .Produces<OpenNewSessionResponse>(200, "application/json")
        );
    }

    public override async Task<OpenNewSessionResponse> ExecuteAsync(OpenNewSessionRequest request, CancellationToken ct)
    {
        var newQuizSession = new DbContext.Entities.QuizSession
        {
            Id = new IdGenerator(0).CreateId(),
            Status = QuizSessionStatus.Open,
            CreatedBy = request.User,
        };
        await _dbContext.QuizSession.Create(newQuizSession);
        return new(newQuizSession.Id);
    }
}