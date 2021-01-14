using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        private readonly ITransactionProvider _transactionProvider;
        
        public ProductController(ITransactionProvider transactionProvider, IMapper mapper, IProductService service)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            Product? post;
            if (string.IsNullOrWhiteSpace(id) ||
                (post = await this._service.GetEntityById(new ObjectId(id))) == null)
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
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            if (string.IsNullOrWhiteSpace(newProduct.Name)
                || string.IsNullOrWhiteSpace(newProduct.Picture)
                || double.IsNaN(newProduct.Weight))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var entity = await this._service.AddEntity(newProduct);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(Product update)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            if (string.IsNullOrWhiteSpace(update.Name)
                || string.IsNullOrWhiteSpace(update.Picture)
                || double.IsNaN(update.Weight))
            {
                return BadRequest();
            }

            using var transaction = await this._transactionProvider.BeginTransaction();
            var entity = await this._service.UpdateEntity(update);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }
        
        [HttpDelete]
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