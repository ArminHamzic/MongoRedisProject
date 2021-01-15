using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LeoMongo.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads
{
    public interface IGenericRepository<T>: IRepositoryBase where T : EntityBase, new()
    {
        IMongoCollection<T> Collection { get; }
        IClientSessionHandle Session { get; }
        UpdateDefinitionBuilder<T> UpdateDefBuilder { get; }
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetEntityByIdAsync(string id);
        Task<T?> GetEntityByIdAsync(ObjectId id);
        Task<T> AddEntity(T entity);
        Task<T> UpdateEntity(T entity);
        Task<bool> DeleteEntity(ObjectId id);
        IMongoQueryable<T> Query();

        IMongoQueryable<MasterDetails<ObjectId, ObjectId>> QueryIncludeDetail<TDetail>(
            IRepositoryBase detailRepository,
            Expression<Func<TDetail, ObjectId>> foreignKeySelector,
            Expression<Func<T, bool>>? masterFilter = null)
            where TDetail : EntityBase;

        IMongoQueryable<MasterDetails<TMasterField, TDetailField>> QueryIncludeDetail<TDetail, TMasterField,
            TDetailField>(
            IRepositoryBase detailRepository,
            Expression<Func<TDetail, ObjectId>> foreignKeySelector,
            Expression<Func<T, IEnumerable<TDetail>, MasterDetails<TMasterField, TDetailField>>> resultSelector,
            Expression<Func<T, bool>>? masterFilter = null)
            where TDetail : EntityBase;

        Task<T> InsertOneAsync(T document);
        Task<IReadOnlyCollection<T>> InsertManyAsync(IReadOnlyCollection<T> documents);
        Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> updateDefinition);

        Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter,
            UpdateDefinition<T> updateDefinition);

        Task<ReplaceOneResult> ReplaceOneAsync(ObjectId id, T document);
        Task<DeleteResult> DeleteOneAsync(ObjectId id);
        Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter);
        IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName);
    }
}