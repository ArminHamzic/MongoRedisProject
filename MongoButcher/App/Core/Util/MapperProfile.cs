using AutoMapper;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;
using MongoDBDemoApp.Model.Comment;
using MongoDBDemoApp.Model.Post;

namespace MongoDBDemoApp.Core.Util
{
    public sealed class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(p => p.Id, c => c.MapFrom(p => p.Id.ToString()));
            CreateMap<Comment, CommentDTO>();
        }
    }
}