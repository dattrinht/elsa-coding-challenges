namespace QuizSession.API.Features.SessionRuntime;

public record StartSessionRequest(string User);

public record StartSessionResponse(bool IsSuccess);

public class StartSession(QuizSessionDbContext dbContext) : Endpoint<StartSessionRequest, Results<BadRequest, Ok<StartSessionResponse>>>
{
    private readonly QuizSessionDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/v1/session-runtime/{id}/start");
        AllowAnonymous();
        Description(b => b
            .Produces(400)
            .Produces<StartSessionResponse>(200, "application/json")
        );
    }

    public override async Task<Results<BadRequest, Ok<StartSessionResponse>>> ExecuteAsync(StartSessionRequest request, CancellationToken ct)
    {
        var quizSessionId = Route<long>("id");
        var quizSession = await _dbContext.QuizSession.GetById(quizSessionId);
        if (quizSession is null || quizSession.Status != QuizSessionStatus.Open)
        {
            return TypedResults.BadRequest();
        }

        if (quizSession.CreatedBy != request.User)
        {
            return TypedResults.BadRequest();
        }

        quizSession.Status = QuizSessionStatus.Started;
        var result = await _dbContext.QuizSession.Update(quizSession);
        await KafkaProducer.Produce("quiz.session.sessionStarted.v1", quizSessionId, new SessionStarted(quizSessionId));
        return TypedResults.Ok(new StartSessionResponse(result));
    }
}