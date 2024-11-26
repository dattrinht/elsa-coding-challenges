namespace QuizSession.API.DbContext.Migration;

public class DatabaseMigration(IOptions<AppSettings> appSettings)
{
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task Run()
    {
        await QuizSession_InitTable();
        await Participant_InitTable();
    }

    private async Task QuizSession_InitTable()
    {
        var dbSettings = _appSettings.DbSettings;
        using var connection = new NpgsqlConnection(dbSettings.ConnectionString);

        await connection.ExecuteAsync("""
                CREATE TABLE IF NOT EXISTS quiz_session (
                    id BIGINT PRIMARY KEY,
                    status VARCHAR(20),
                    created_by VARCHAR(20),
                    created_at TIMESTAMP DEFAULT NOW()
                );
                """);
    }

    private async Task Participant_InitTable()
    {
        var dbSettings = _appSettings.DbSettings;
        using var connection = new NpgsqlConnection(dbSettings.ConnectionString);

        await connection.ExecuteAsync("""
                CREATE TABLE IF NOT EXISTS participant (
                    id BIGINT PRIMARY KEY,
                    quiz_session_id BIGINT,
                    user_name VARCHAR(20),
                    created_at TIMESTAMP DEFAULT NOW()
                );
                """);
    }
}

public static class MigrationExtension
{
    public static void UseMigrationDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var migration = scope.ServiceProvider.GetService<DatabaseMigration>();
        migration!.Run().GetAwaiter().GetResult();
    }
}