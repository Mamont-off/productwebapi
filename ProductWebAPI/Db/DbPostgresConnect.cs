namespace ProductWebAPI;

internal class DbPostgresConnect
{
    internal string ConnectionString;
    internal DbPostgresConnect()
    {
        ConnectionString = CreateConnectionString();
    }

    private string CreateConnectionString()
    {
        var dbname = Environment.GetEnvironmentVariable("DB_NAME");
        var host = Environment.GetEnvironmentVariable("HOST");
        var port = Environment.GetEnvironmentVariable("PORT");
        var username = Environment.GetEnvironmentVariable("USERNAME");
        var password = Environment.GetEnvironmentVariable("PASSWORD");
        
        return $"Host={host};Port={port};Database={dbname};Username={username};Password={password}";
    }
}