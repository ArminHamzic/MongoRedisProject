using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IResourceService _service;
        private readonly ITransactionProvider _transactionProvider;

        public ResourceController(ITransactionProvider transactionProvider, IMapper mapper,
            IResourceService service)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<Resource>> GetById(string id)
        {
            Resource? entity;
            if (string.IsNullOrWhiteSpace(id) ||
                (entity = await this._service.GetEntityById(new ObjectId(id))) == null)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<Resource>>> GetAll()
        {
            IEnumerable<Resource> posts = await this._service.GetAll();
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Resource newEntity)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            using var transaction = await this._transactionProvider.BeginTransaction();
            
            var entity = await this._service.AddEntity(newEntity);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Resource update)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this

            using var transaction = await this._transactionProvider.BeginTransaction();

            var resource = await _service.GetResourceByProductName(update.Product.Name);

            if (resource == null)
            {
                return NotFound();
            }
            
            resource.Amount += update.Amount;
            resource = await this._service.UpdateEntity(resource);
            
            await transaction.CommitAsync();
            
            return Ok(resource);
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