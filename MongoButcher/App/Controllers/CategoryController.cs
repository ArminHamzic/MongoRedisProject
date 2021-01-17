using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Categories;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _service;
        private readonly ITransactionProvider _transactionProvider;

        public CategoryController(ITransactionProvider transactionProvider, IMapper mapper,
            ICategoryService service)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetById(string id)
        {
            Category? entity;
            if (string.IsNullOrWhiteSpace(id) ||
                (entity = await this._service.GetEntityById(id)) == null)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<Category>>> GetAll()
        {
            IEnumerable<Category> entities = await this._service.GetAll();
            return Ok(entities);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category newEntity)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this

            using var transaction = await this._transactionProvider.BeginTransaction();
            
            var entity = await this._service.AddEntity(newEntity);
            await transaction.CommitAsync();
            
            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category update)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this

            using var transaction = await this._transactionProvider.BeginTransaction();
            
            var entity = await this._service.UpdateEntity(update);
            await transaction.CommitAsync();
            
            return Ok(entity);
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