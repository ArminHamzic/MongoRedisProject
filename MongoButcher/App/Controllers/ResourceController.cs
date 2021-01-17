using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Model.Resource;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.ActionHistories;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IResourceService _service;
        private readonly IProductService _productService;
        private readonly ITransactionProvider _transactionProvider;

        public ResourceController(ITransactionProvider transactionProvider,
            IMapper mapper,
            IResourceService service,
            IProductService productService)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
            _productService = productService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ResourceDTO>> GetById(string id)
        {
            Resource? entity;
            if (string.IsNullOrWhiteSpace(id) ||
                (entity = await this._service.GetEntityById(id)) == null)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<ResourceDTO>>> GetAll()
        {
            IEnumerable<Resource> resources = await this._service.GetAll();
            return Ok(resources
                .Select(async resource => new ResourceDTO
                {
                    Product = await _productService.GetByName(resource.ProductName),
                    Amount = resource.Amount
                })
                .Select(task => task.Result)
                .ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResourceDTO newEntity)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this
            using var transaction = await this._transactionProvider.BeginTransaction();

            var entity = await this._service.AddEntity(new Resource
            {
                Amount = newEntity.Amount,
                ProductName = newEntity.Product.Name,
                ActionHistories = new List<ActionHistory>()
            });
            await transaction.CommitAsync();
            return Ok(new ResourceDTO
            {
                Amount = entity.Amount,
                Product = await _productService.GetByName(entity.ProductName),
                ActionHistories = entity.ActionHistories
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(ResourceDTO update)
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

            return Ok(new ResourceDTO
            {
                Amount = resource.Amount, 
                Product = await _productService.GetByName(update.Product.Name),
                ActionHistories = resource.ActionHistories
            });
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