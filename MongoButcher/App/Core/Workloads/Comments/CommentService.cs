using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Posts;

namespace MongoDBDemoApp.Core.Workloads.Comments
{
    public sealed class CommentService : ICommentService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ICommentRepository _repository;

        public CommentService(IDateTimeProvider dateTimeProvider, ICommentRepository repository)
        {
            this._dateTimeProvider = dateTimeProvider;
            this._repository = repository;
        }

        public Task<IReadOnlyCollection<Comment>> GetCommentsForPost(Post post) =>
            this._repository.GetCommentsForPost(post.Id);

        public Task<Comment> AddComment(Post post, string name, string mail, string text)
        {
            // TODO
        }

        public Task<Comment?> GetCommentById(ObjectId id) => this._repository.GetCommentById(id);
    }
}