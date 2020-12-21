using System.Collections.Generic;
using System.Threading.Tasks;
using LeoMongo.Database;
using MongoDB.Bson;

namespace MongoDBDemoApp.Core.Workloads.Comments
{
    public interface ICommentRepository : IRepositoryBase
    {
        Task<IReadOnlyCollection<Comment>> GetCommentsForPost(ObjectId postId);
        Task<Comment> AddComment(Comment comment);
        Task<Comment?> GetCommentById(ObjectId id);
        Task DeleteCommentsByPost(ObjectId postId);
    }
}