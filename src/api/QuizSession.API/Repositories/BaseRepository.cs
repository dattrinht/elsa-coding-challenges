namespace QuizSession.API.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    Task<T?> GetById(long id);
    Task<IEnumerable<T>> GetByQuery(string query);
    Task Delete(long id);
    Task<int> Count(string? condition = null);
}

public class BaseRepository<T>(IDbConnectionFactory factory, string tableName) where T : Entity
{
    protected readonly string _tableName = tableName;
    protected readonly IDbConnectionFactory _factory = factory;

    public virtual async Task<T?> GetById(long id)
    {
        using var connection = _factory.CreateConnection();
        var sql = $"""
        SELECT * FROM {_tableName} 
        WHERE Id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<T>(sql, new { id });
    }

    public virtual async Task<IEnumerable<T>> GetByQuery(string query)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QueryAsync<T>(query);
    }

    public virtual async Task Delete(long id)
    {
        using var connection = _factory.CreateConnection();
        var sql = $"""
        DELETE FROM {_tableName}
        WHERE Id = @id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }

    public virtual async Task<int> Count(string? filterQuery = null)
    {
        using var connection = _factory.CreateConnection();
        var sql = $"""
        SELECT COUNT(0)
        FROM {_tableName}

        """;

        sql += filterQuery;

        var result = await connection.ExecuteScalarAsync<int>(sql);
        return result;
    }
}