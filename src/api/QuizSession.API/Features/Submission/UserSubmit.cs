namespace QuizSession.API.Features.Submission;

public record UserSubmitRequest(long QuizSessionId, long ParticipantId, bool IsCorrect);

public record UserSubmitResponse(bool IsSuccess);

public class UserSubmit(QuizSessionDbContext dbContext) : Endpoint<UserSubmitRequest, Results<BadRequest, Ok<UserSubmitResponse>>>
{
    private readonly QuizSessionDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/v1/submit");
        AllowAnonymous();
        Description(b => b
            .Produces(400)
            .Produces<UserSubmitResponse>(200, "application/json")
        );
    }

    public override async Task<Results<BadRequest, Ok<UserSubmitResponse>>> ExecuteAsync(UserSubmitRequest request, CancellationToken ct)
    {
        var quizSessionId = request.QuizSessionId;
        // Mock data for userSubmmitted
        await KafkaProducer.Produce("quiz.session.userSubmitted.v1", quizSessionId, new UserSubmitted(quizSessionId, request.ParticipantId, request.IsCorrect));
        return TypedResults.Ok(new UserSubmitResponse(true));
    }
}