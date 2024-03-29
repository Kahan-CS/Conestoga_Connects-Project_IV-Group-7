namespace BlazorApp.Server.Models
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } = "MongoDatabase";
    }
}
