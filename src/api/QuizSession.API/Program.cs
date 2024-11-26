var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services
    .AddFastEndpoints()
    .SwaggerDocument();

services.AddCors(opt => opt.AddPolicy("AllowAll", policy =>
{
    policy.AllowAnyOrigin();
    policy.AllowAnyMethod();
    policy.AllowAnyHeader();
}));

services.Configure<AppSettings>(builder.Configuration);

services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
services.AddSingleton<Seed>();
services.AddSingleton<DatabaseMigration>();

services.AddScoped<IQuizSessionRepository, QuizSessionRepository>();
services.AddScoped<IParticipantRepository, ParticipantRepository>();
services.AddScoped<QuizSessionDbContext>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSeedingDatabase();
app.UseMigrationDatabase();

app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.MapGet("/", () => "Hello!!");

app.Run("http://*:5003");