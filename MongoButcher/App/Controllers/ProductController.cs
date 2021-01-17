using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.ActionHistories;
using MongoDBDemoApp.Core.Workloads.Categories;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        private readonly IResourceService _resourceService;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionProvider _transactionProvider;

        public ProductController(ITransactionProvider transactionProvider, IMapper mapper,
            IProductService service,
            ICategoryService categoryService,
            IResourceService resourceService)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
            _resourceService = resourceService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            Product? post;
            if (string.IsNullOrWhiteSpace(id) ||
                (post = await this._service.GetEntityById(id)) == null)
            {
                return BadRequest();
            }

            return Ok(post);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetAll()
        {
            IEnumerable<Product> posts = await this._service.GetAll();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product newProduct)
        {
            if (string.IsNullOrWhiteSpace(newProduct.Name)
                || string.IsNullOrWhiteSpace(newProduct.Picture))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();

            var category = await _categoryService.GetOrCreateCategoryByName(newProduct.Category);
            newProduct.Category = category;

            var entity = await this._service.AddEntity(newProduct);
            var newResource = new Resource
                {Amount = 0, ProductName = entity.Name, ActionHistories = new List<ActionHistory>()};

            await _resourceService.AddEntity(newResource);

            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product update)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            if (string.IsNullOrWhiteSpace(update.Name)
                || string.IsNullOrWhiteSpace(update.Picture))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var entity = await this._service.UpdateEntity(update);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            await this._service.DeleteEntity(new ObjectId(id));
            await transaction.CommitAsync();
            return Ok();
        }
    }
}