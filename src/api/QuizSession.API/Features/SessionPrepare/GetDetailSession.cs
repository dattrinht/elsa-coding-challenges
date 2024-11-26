namespace QuizSession.API.Features.SessionPrepare;

public record GetDetailSessionResponse(long Id, string Status, string CreatedBy, string CreatedAt)
{
    public IEnumerable<GetPaticipantResponse>? Paticipants { get; set; }
};

public record GetPaticipantResponse(long Id, string User);

public class GetDetailSession(QuizSessionDbContext dbContext) : EndpointWithoutRequest<Results<NotFound, Ok<GetDetailSessionResponse>>>
{
    private readonly QuizSessionDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/v1/session-prepare/{id}/detail");
        AllowAnonymous();
        Description(b => b
            .Produces(404)
            .Produces<GetDetailSessionResponse>(200, "application/json")
        );
    }

    public override async Task<Results<NotFound, Ok<GetDetailSessionResponse>>> ExecuteAsync(CancellationToken ct)
    {
        var id = Route<long>("id");
        var response = await _dbContext.QuizSession.GetById(id);
        if (response is null)
        {
            return TypedResults.NotFound();
        }
        var mapper = new GetDetailSessionMapper();
        var dto = mapper.EntityToDto(response!);
        var participantsQuery = $"""
            SELECT * FROM participant
            WHERE quiz_session_id = {id}
            """;
        var participants = await _dbContext.Participant.GetByQuery(participantsQuery);
        dto.Paticipants = participants?.Select(x => new GetPaticipantResponse(x.Id, x.UserName));
        return TypedResults.Ok(dto);
    }
}

[Mapper]
public partial class GetDetailSessionMapper
{
#pragma warning disable RMG012 // Source member was not found for target member
    public partial GetDetailSessionResponse EntityToDto(DbContext.Entities.QuizSession entity);
#pragma warning restore RMG012 // Source member was not found for target member
}