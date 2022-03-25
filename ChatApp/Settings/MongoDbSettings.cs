namespace ChatApp.Settings;

public class MongoDbSettings
{
    public string Host { get; init; } = string.Empty;

    public int Port { get; init; }

    public string Database { get; init; } = string.Empty;

    public string ConnectionString => $"mongodb://{Host}:{Port}";
}