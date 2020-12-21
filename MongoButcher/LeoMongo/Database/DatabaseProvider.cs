using System.Threading.Tasks;
using MongoDB.Driver;

namespace LeoMongo.Database
{
    internal sealed class DatabaseProvider : IDatabaseProvider
    {
        private readonly MongoClient _client;

        public DatabaseProvider(IMongoConfig options)
        {
            this._client = new MongoClient(options.ConnectionString);
            Database = this._client.GetDatabase(options.DatabaseName);
        }

        public IMongoDatabase Database { get; }

        public Task<IClientSessionHandle> StartSession() => this._client.StartSessionAsync();
    }
}