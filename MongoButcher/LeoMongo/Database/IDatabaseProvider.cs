using System.Threading.Tasks;
using MongoDB.Driver;

namespace LeoMongo.Database
{
    public interface IDatabaseProvider
    {
        IMongoDatabase Database { get; }
        Task<IClientSessionHandle> StartSession();
    }
}