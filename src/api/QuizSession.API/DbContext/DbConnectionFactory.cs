namespace QuizSession.API.DbContext;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class DbConnectionFactory(IOptions<AppSettings> appSettings) : IDbConnectionFactory
{
    private readonly AppSettings _appSettings = appSettings.Value;

    public IDbConnection CreateConnection()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        return new NpgsqlConnection(_appSettings.DbSettings.ConnectionString);
    }
}