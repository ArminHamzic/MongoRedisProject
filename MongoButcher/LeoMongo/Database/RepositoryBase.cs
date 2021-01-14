using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LeoMongo.Database
{
    public abstract class RepositoryBase<T> : IRepositoryBase where T : EntityBase
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly ITransactionProvider _transactionProvider;
        private IMongoCollection<T>? _collection;

        protected RepositoryBase(ITransactionProvider transactionProvider,
            IDatabaseProvider databaseProvider)
        {
            this._transactionProvider = transactionProvider;
            this._databaseProvider = databaseProvider;
        }

        public IMongoCollection<T> Collection =>
            this._collection ??= GetCollection<T>(CollectionName);

        public IClientSessionHandle Session
        {
            get
            {
                var sessionProvider = (ISessionProvider) this._transactionProvider;
                return sessionProvider.Session;
            }
        }

        public UpdateDefinitionBuilder<T> UpdateDefBuilder => new UpdateDefinitionBuilder<T>();

        public abstract string CollectionName { get; }

        public IMongoQueryable<T> Query()
        {
            if (this._transactionProvider.InTransaction)
            {
                return Collection.AsQueryable(Session);
            }

            return Collection.AsQueryable();
        }

        public IMongoQueryable<MasterDetails<ObjectId, ObjectId>> QueryIncludeDetail<TDetail>(
            IRepositoryBase detailRepository,
            Expression<Func<TDetail, ObjectId>> foreignKeySelector,
            Expression<Func<T, bool>>? masterFilter = null)
            where TDetail : EntityBase
        {
            return QueryIncludeDetail(detailRepository, foreignKeySelector, (master, details) =>
                new MasterDetails<ObjectId, ObjectId>
                {
                    Master = master.Id,
                    Details = details.Select(d => d.Id)
                }, masterFilter);
        }

        public IMongoQueryable<MasterDetails<TMasterField, TDetailField>> QueryIncludeDetail<TDetail, TMasterField,
            TDetailField>(
            IRepositoryBase detailRepository,
            Expression<Func<TDetail, ObjectId>> foreignKeySelector,
            Expression<Func<T, IEnumerable<TDetail>, MasterDetails<TMasterField, TDetailField>>> resultSelector,
            Expression<Func<T, bool>>? masterFilter = null)
            where TDetail : EntityBase
        {
            if (typeof(TMasterField) == typeof(T)
                || typeof(TDetailField) == typeof(TDetail))
            {
                throw new NotSupportedException("A projection must not include the document itself (MongoDB Driver limitation)");
            }

            // automatically applies transaction
            IMongoQueryable<T> query = Query();

            if (masterFilter != null)
            {
                query = query.Where(masterFilter);
            }

            IMongoQueryable<MasterDetails<TMasterField, TDetailField>> joinedQuery = query
                .GroupJoin(GetCollection<TDetail>(detailRepository.CollectionName), m => m.Id,
                    foreignKeySelector, resultSelector);
            return joinedQuery;
        }

        // it WOULD be awesome to get the master and detail documents in such a way
        // BUT a Projection (in MongoDB Driver) MAY NOT include the document itself, so this will lead to an error
        // see https://stackoverflow.com/questions/47383632/project-or-group-does-not-support-document
        //
        //protected IMongoQueryable<MasterDetails<T, TDetail>> QueryIncludeDetail<TDetail>(
        //    IRepositoryBase detailRepository,
        //    Expression<Func<TDetail, ObjectId>> foreignKeySelector, Expression<Func<T, bool>>? masterFilter = null)
        //    where TDetail : EntityBase
        //{
        //    // automatically applies transaction
        //    IMongoQueryable<T> query = Query();

        //    if (masterFilter != null)
        //    {
        //        query = query.Where(masterFilter);
        //    }

        //    IMongoQueryable<MasterDetails<T, TDetail>> joinedQuery = query
        //        .GroupJoin(GetCollection<TDetail>(detailRepository.CollectionName), m => m.Id, foreignKeySelector,
        //            (master, details) => new MasterDetails<T, TDetail>
        //            {
        //                Master = master,
        //                Details = details
        //            });
        //    return joinedQuery;
        //}

        public async Task<T> InsertOneAsync(T document)
        {
            if (this._transactionProvider.InTransaction)
            {
                await Collection.InsertOneAsync(Session, document);
            }
            else
            {
                await Collection.InsertOneAsync(document);
            }

            return document;
        }

        public async Task<IReadOnlyCollection<T>> InsertManyAsync(IReadOnlyCollection<T> documents)
        {
            if (this._transactionProvider.InTransaction)
            {
                await Collection.InsertManyAsync(Session, documents);
            }
            else
            {
                await Collection.InsertManyAsync(documents);
            }

            return documents;
        }

        public Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> updateDefinition)
        {
            return this._transactionProvider.InTransaction
                ? Collection.UpdateOneAsync(Session, GetIdFilter(id), updateDefinition)
                : Collection.UpdateOneAsync(GetIdFilter(id), updateDefinition);
        }

        public Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter,
            UpdateDefinition<T> updateDefinition)
        {
            return this._transactionProvider.InTransaction
                ? Collection.UpdateManyAsync(Session, filter, updateDefinition)
                : Collection.UpdateManyAsync(filter, updateDefinition);
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(ObjectId id, T document)
        {
            return this._transactionProvider.InTransaction
                ? Collection.ReplaceOneAsync(Session, GetIdFilter(id), document)
                : Collection.ReplaceOneAsync(GetIdFilter(id), document);
        }

        public Task<DeleteResult> DeleteOneAsync(ObjectId id)
        {
            return this._transactionProvider.InTransaction
                ? Collection.DeleteOneAsync(Session, GetIdFilter(id))
                : Collection.DeleteOneAsync(GetIdFilter(id));
        }

        public Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            return this._transactionProvider.InTransaction
                ? Collection.DeleteManyAsync(Session, filter)
                : Collection.DeleteManyAsync(filter);
        }

        public IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName) =>
            this._databaseProvider.Database.GetCollection<TCollection>(collectionName);

        private static Expression<Func<T, bool>> GetIdFilter(ObjectId id) => t => t.Id == id;
    }
}