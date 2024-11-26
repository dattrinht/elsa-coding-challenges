namespace QuizSession.API.Repositories;

public interface IQuizSessionRepository : IBaseRepository<DbContext.Entities.QuizSession>
{
    Task<long> Create(DbContext.Entities.QuizSession entity);
    Task<bool> Update(DbContext.Entities.QuizSession entity);
}

public class QuizSessionRepository(IDbConnectionFactory factory) :
      BaseRepository<DbContext.Entities.QuizSession>(factory, "quiz_session")
    , IQuizSessionRepository
{
    public async Task<long> Create(DbContext.Entities.QuizSession entity)
    {
        using var connection = _factory.CreateConnection();
        var sql = $"""
        INSERT INTO {_tableName} (id, status, created_by)
        VALUES (@Id, @Status, @CreatedBy)
        """;
        await connection.ExecuteAsync(sql, entity);
        return entity.Id;
    }

    public async Task<bool> Update(DbContext.Entities.QuizSession entity)
    {
        using var connection = _factory.CreateConnection();
        var sql = $"""
        UPDATE {_tableName} 
        SET status = @Status
        WHERE id = @Id
        """;
        var result = await connection.ExecuteAsync(sql, entity);
        return result > 0;
    }
}
