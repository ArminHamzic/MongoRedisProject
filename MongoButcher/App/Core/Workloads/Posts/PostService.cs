using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;

namespace MongoDBDemoApp.Core.Workloads.Posts
{
    public sealed class PostService : IPostService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<PostService> _logger;
        private readonly IPostRepository _repository;

        public PostService(IDateTimeProvider dateTimeProvider, IPostRepository repository,
            ICommentRepository commentRepository, ILogger<PostService> logger)
        {
            this._dateTimeProvider = dateTimeProvider;
            this._repository = repository;
            this._commentRepository = commentRepository;
            this._logger = logger;
        }

        public Task<IReadOnlyCollection<Post>> GetAllPosts() => this._repository.GetAllPosts();

        public Task<Post?> GetPostById(ObjectId id) => this._repository.GetPostById(id);

        public Task<Post> AddPost(string title, string author, string text)
        {
            var post = new Post
            {
                Author = author,
                Published = this._dateTimeProvider.Now,
                Text = text,
                Title = title,
                UpVotes = 0
            };
            return this._repository.AddPost(post);
        }

        public async Task DeletePost(ObjectId id)
        {
            (ObjectId PostId, List<ObjectId>? CommentIds)? postWithComments =
                await this._repository.GetPostWithComments(id);
            if (postWithComments == null)
            {
                throw new ArgumentException(nameof(id));
            }

            List<ObjectId>? comments = postWithComments.Value.CommentIds;
            if (comments != null && comments.Count > 0)
            {
                this._logger.LogInformation($"Deleting {comments.Count} comment(s) together with post {id}.");
                await this._commentRepository.DeleteCommentsByPost(id);
            }

            await this._repository.DeletePost(postWithComments.Value.PostId);
        }
    }
}