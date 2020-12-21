namespace MongoDBDemoApp.Core.Util
{
    public sealed class AppSettings
    {
        public const string KEY = "AppSettings";

        public string ConnectionString { get; set; } = default!;
        public string Database { get; set; } = default!;
    }
}