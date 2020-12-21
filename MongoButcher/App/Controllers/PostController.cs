﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Posts;
using MongoDBDemoApp.Model.Post;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _service;
        private readonly ITransactionProvider _transactionProvider;

        public PostController(ITransactionProvider transactionProvider, IMapper mapper, IPostService service)
        {
            this._transactionProvider = transactionProvider;
            this._mapper = mapper;
            this._service = service;
        }

        /// <summary>
        ///     Returns the post identified by the given id if it exists.
        /// </summary>
        /// <param name="id">existing post id</param>
        /// <returns>a post</returns>
        [HttpGet]
        public async Task<ActionResult<PostDTO>> GetById(string id)
        {
            Post? post;
            if (string.IsNullOrWhiteSpace(id) ||
                (post = await this._service.GetPostById(new ObjectId(id))) == null)
            {
                return BadRequest();
            }

            return Ok(this._mapper.Map<PostDTO>(post));
        }

        /// <summary>
        ///     Returns all posts.
        ///     In a real world application you'd need pagination here.
        /// </summary>
        /// <returns>All existing posts</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<PostDTO>>> GetAll()
        {
            const int TEASER_LENGTH = 250;

            IReadOnlyCollection<Post> posts = await this._service.GetAllPosts();
            return Ok(this._mapper.Map<List<PostDTO>>(posts,
                options =>
                {
                    options.AfterMap((_, pl) =>
                        pl.ForEach(p => p.Text = p.Text.Truncate(TEASER_LENGTH)));
                }));
        }

        /// <summary>
        ///     Removes the post identified by the given id.
        ///     If the id does not exist nothing will be changed.
        /// </summary>
        /// <param name="id">if of an existing post</param>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            await this._service.DeletePost(new ObjectId(id));
            await transaction.CommitAsync();
            return Ok();
        }

        /// <summary>
        ///     Creates a new post.
        /// </summary>
        /// <param name="request">Data for the new post</param>
        /// <returns>the created post if successful</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            if (string.IsNullOrWhiteSpace(request.Title)
                || string.IsNullOrWhiteSpace(request.Author)
                || string.IsNullOrWhiteSpace(request.Text))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var post = await this._service.AddPost(request.Title, request.Author, request.Text);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = post.Id.ToString()}, post);
        }
    }
}