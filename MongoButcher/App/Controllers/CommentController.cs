using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;
using MongoDBDemoApp.Model.Comment;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly ICommentService _service;
        private readonly ITransactionProvider _transactionProvider;

        public CommentController(IMapper mapper, ICommentService service, ITransactionProvider transactionProvider,
            IPostService postService)
        {
            this._mapper = mapper;
            this._service = service;
            this._transactionProvider = transactionProvider;
            this._postService = postService;
        }

        /// <summary>
        ///     Returns all comments for the post with the given id.
        /// </summary>
        /// <param name="postId">id of an existing post</param>
        /// <returns>List of comments, may be empty</returns>
        [HttpGet]
        [Route("post")]
        public async Task<ActionResult<IReadOnlyCollection<CommentDTO>>> GetCommentsForPost(string postId)
        {
            Post? post;
            if (string.IsNullOrWhiteSpace(postId) ||
                (post = await this._postService.GetPostById(new ObjectId(postId))) == null)
            {
                return BadRequest();
            }

            IReadOnlyCollection<Comment>? comments = await this._service.GetCommentsForPost(post);
            return Ok(this._mapper.Map<List<CommentDTO>>(comments));
        }

        /// <summary>
        ///     Returns the comment identified by the given id if it exists.
        /// </summary>
        /// <param name="id">existing comment id</param>
        /// <returns>a comment</returns>
        [HttpGet]
        public async Task<ActionResult<CommentDTO>> GetById(string id)
        {
            Comment? comment;
            if (string.IsNullOrWhiteSpace(id)
                || (comment = await this._service.GetCommentById(new ObjectId(id))) == null)
            {
                return BadRequest();
            }

            return Ok(this._mapper.Map<CommentDTO>(comment));
        }

        /// <summary>
        ///     Creates a new comment.
        /// </summary>
        /// <param name="request">Data for the new comment</param>
        /// <returns>the created comment if successful</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
        {
            // TODO
        }
    }
}