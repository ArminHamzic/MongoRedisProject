using System.Collections.Generic;
using System.Threading.Tasks;
using LeoMongo.Database;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads
{
    public abstract class GenericService<T> : IGenericService<T> 
        where T : EntityBase, new()
    {
        protected readonly IGenericRepository<T> Repository;
        protected readonly IDateTimeProvider DateTimeProvider;
        protected readonly ILogger<GenericService<T>> Logger;

        public GenericService(IDateTimeProvider dateTimeProvider, IGenericRepository<T> repository,
            ILogger<GenericService<T>> logger)
        {
            Repository = repository;
            DateTimeProvider = dateTimeProvider;
            Logger = logger;
        }

        public Task<IEnumerable<T>> GetAll() => Repository.GetAll();
        public Task<T?> GetEntityById(string id)
        {
            return Repository.GetEntityByIdAsync(id);
        }

        public Task<T?> GetEntityById(ObjectId id)
        {
            return Repository.GetEntityByIdAsync(id);
        }

        public Task<T> AddEntity(T entity)
        {
            return Repository.AddEntity(entity);
        }

        public Task<T> UpdateEntity(T entity)
        {
            return Repository.UpdateEntity(entity);
        }

        public Task DeleteEntity(ObjectId id)
        {
            return Repository.DeleteEntity(id);
        }
    }
}