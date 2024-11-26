namespace QuizSession.API;

public class AppSettings
{
    public required DbSettings DbSettings { get; set; }
    public required KafkaSettings KafkaSettings { get; set; }
}

public class DbSettings
{
    private string? _host;
    public required string Host
    {
        get
        {
#if DEBUG
            _host = "localhost";
#endif
            return _host;
        }
        set
        {
            _host = value;
        }
    }
    public short Port { get; set; }
    public required string Database { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string ConnectionString
        => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
}

public class KafkaSettings
{
    public required string Host { get; set; }
}