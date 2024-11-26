namespace QuizSession.API.Repositories;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    Task<long> Create(Participant entity);
}

public class ParticipantRepository(IDbConnectionFactory factory) :
      BaseRepository<Participant>(factory, "participant")
    , IParticipantRepository
{
    public async Task<long> Create(Participant entity)
    {
        using var connection = _factory.CreateConnection();
        var sql = $"""
        INSERT INTO {_tableName} (id, quiz_session_id, user_name)
        VALUES (@Id, @QuizSessionId, @UserName)
        """;
        await connection.ExecuteAsync(sql, entity);
        return entity.Id;
    }
}
