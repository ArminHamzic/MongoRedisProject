using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Posts;

namespace MongoDBDemoApp.Core.Workloads.Comments
{
    public interface ICommentService
    {
        Task<IReadOnlyCollection<Comment>> GetCommentsForPost(Post post);
        Task<Comment> AddComment(Post post, string name, string mail, string text);
        Task<Comment?> GetCommentById(ObjectId id);
    }
}