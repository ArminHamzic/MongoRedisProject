using System.Collections.Generic;
using System.Threading.Tasks;
using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads
{
    public interface IGenericService<T> where T : EntityBase, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetEntityById(string id);
        Task<T?> GetEntityById(ObjectId id);
        Task<T> AddEntity(T entity);
        Task<T> UpdateEntity(T entity);
        Task DeleteEntity(ObjectId id);
    }
}