using System.Collections.Generic;
using System.Threading.Tasks;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Comments
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }

        public override string CollectionName { get; } = MongoUtil.GetCollectionName<Comment>();

        public async Task<IReadOnlyCollection<Comment>> GetCommentsForPost(ObjectId postId)
        {
            // TODO
        }

        public Task<Comment> AddComment(Comment comment)
        {
            // TODO
        }

        public async Task<Comment?> GetCommentById(ObjectId id)
        {
            // TODO
        }

        public Task DeleteCommentsByPost(ObjectId postId)
        {
            // TODO
        }
    }
}