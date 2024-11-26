namespace QuizSession.API.Features.SessionRuntime;

public record LeaveSessionRequest(string User);

public record LeaveSessionResponse(long Id);

public class LeaveSession(QuizSessionDbContext dbContext) : Endpoint<LeaveSessionRequest, Results<BadRequest, Ok<LeaveSessionResponse>>>
{
    private readonly QuizSessionDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/v1/session-runtime/{id}/leave");
        AllowAnonymous();
        Description(b => b
            .Produces(400)
            .Produces<LeaveSessionResponse>(200, "application/json")
        );
    }

    public override async Task<Results<BadRequest, Ok<LeaveSessionResponse>>> ExecuteAsync(LeaveSessionRequest request, CancellationToken ct)
    {
        var quizSessionId = Route<long>("id");
        var quizSession = await _dbContext.QuizSession.GetById(quizSessionId);
        if (quizSession is null || quizSession.Status != QuizSessionStatus.Open)
        {
            return TypedResults.BadRequest();
        }
        var participantQuery = $"""
            SELECT * FROM participant
            WHERE user_name = '{request.User}' AND quiz_session_id = {quizSessionId}
            """;
        var participants = await _dbContext.Participant.GetByQuery(participantQuery);
        if (participants?.Any() != true)
        {
            return TypedResults.BadRequest();
        }

        var participantId = participants.First().Id;
        await _dbContext.Participant.Delete(participantId);
        return TypedResults.Ok(new LeaveSessionResponse(participantId));
    }
}