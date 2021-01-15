using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads
{
    public abstract class GenericRepository<T> : RepositoryBase<T>, IGenericRepository<T> where T : EntityBase, new()
    {
        public override string CollectionName { get; } = MongoUtil.GetCollectionName<T>();

        public GenericRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider)
            : base(transactionProvider, databaseProvider)
        {
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Query().ToListAsync();
        }
        
        public async Task<T?> GetEntityByIdAsync(string id)
        {
            return await Query().FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<T?> GetEntityByIdAsync(ObjectId id)
        {
            return await Query().FirstOrDefaultAsync(entity => entity.BaseId == id);
        }

        public async Task<T> AddEntity(T entity)
        {
            return await InsertOneAsync(entity);
        }

        public async Task<T> UpdateEntity(T entity)
        {
            await ReplaceOneAsync(entity.BaseId, entity);
            return await Query().FirstOrDefaultAsync(item => item.Id == entity.Id);
        }

        public async Task<bool> DeleteEntity(ObjectId id)
        {
            var feedback = await DeleteOneAsync(id);
            return feedback.IsAcknowledged;
        }
    }
}