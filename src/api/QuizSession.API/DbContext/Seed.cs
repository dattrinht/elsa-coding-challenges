namespace QuizSession.API.DbContext;

public class Seed(IOptions<AppSettings> appSettings)
{
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task Init()
    {
        await InitDatabase();
    }

    private async Task InitDatabase()
    {
        var dbSettings = _appSettings.DbSettings;
        using var connection = new NpgsqlConnection($"Host={dbSettings.Host};Port={dbSettings.Port};Username={dbSettings.Username};Password={dbSettings.Password}");
        var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{dbSettings.Database}';";
        var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
        if (dbCount == 0)
        {
            var sql = $"CREATE DATABASE {dbSettings.Database}";
            await connection.ExecuteAsync(sql);
        }
    }
}

public static class SeedExtension
{
    public static void UseSeedingDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var seed = scope.ServiceProvider.GetService<Seed>();
        seed!.Init().GetAwaiter().GetResult();
    }
}