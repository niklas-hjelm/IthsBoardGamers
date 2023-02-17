namespace IthsBoardGamers.DataAccess;

public class DatabaseOptions
{
    public const string SectionName = "MongoDbSettings";
    public string Host { get; set; }
    public string Database { get; set; }
    public int Port { get; set; }
}