using Confluent.Kafka;

namespace QuizSession.API.Features.SessionRuntime;

public record JoinSessionRequest(string User);

public record JoinSessionResponse(string Id);

public class JoinSession(QuizSessionDbContext dbContext)
    : Endpoint<JoinSessionRequest, Results<BadRequest, Ok<JoinSessionResponse>>>
{
    private readonly QuizSessionDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/v1/session-runtime/{id}/join");
        AllowAnonymous();
        Description(b => b
            .Produces(400)
            .Produces<JoinSessionResponse>(200, "application/json")
        );
    }

    public override async Task<Results<BadRequest, Ok<JoinSessionResponse>>> ExecuteAsync(JoinSessionRequest request, CancellationToken ct)
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
        if (participants?.Any() == true)
        {
            return TypedResults.Ok(new JoinSessionResponse(participants.FirstOrDefault()!.Id.ToString()));
        }

        var newParticipant = new Participant
        {
            Id = new IdGenerator(0).CreateId(),
            QuizSessionId = quizSessionId,
            UserName = request.User,
        };
        await _dbContext.Participant.Create(newParticipant);
        await KafkaProducer.Produce("quiz.session.userJoined.v1", quizSessionId, new UserJoined(quizSessionId, newParticipant.Id, request.User));
        return TypedResults.Ok(new JoinSessionResponse(newParticipant!.Id.ToString()));
    }
}