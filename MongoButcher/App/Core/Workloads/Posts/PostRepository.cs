using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeoMongo;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;

namespace MongoDBDemoApp.Core.Workloads.Posts
{
    public sealed class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        private readonly ICommentRepository _commentRepository;

        public PostRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider,
            ICommentRepository commentRepository) : base(
            transactionProvider, databaseProvider)
        {
            this._commentRepository = commentRepository;
        }

        public override string CollectionName { get; } = MongoUtil.GetCollectionName<Post>();

        public async Task<IReadOnlyCollection<Post>> GetAllPosts()
        {
            return await Query().ToListAsync();
        }

        public async Task<Post?> GetPostById(ObjectId id)
        {
            // TODO
        }

        public Task<Post> AddPost(Post post)
        {
            // TODO
        }

        public Task DeletePost(ObjectId id)
        {
            // TODO
        }

        public async Task<(ObjectId PostId, List<ObjectId>? CommentIds)?> GetPostWithComments(ObjectId postId)
        {
            // TODO
            // provided because a little complex - BUT try to understand the implementation (also in the Fwk!!)
            IDictionary<ObjectId, List<ObjectId>?> postsWithComments = await QueryIncludeDetail<Comment>(
                    this._commentRepository,
                    c => c.PostId, p => p.Id == postId)
                .ToDictionaryAsync();
            if (postsWithComments.Count == 0
                || postsWithComments.Keys.All(p => p != postId))
            {
                return null;
            }

            return postsWithComments.First().ToTuple();
        }
    }
}