using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Recipes;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecipeService _service;
        private readonly ITransactionProvider _transactionProvider;

        public RecipeController(ITransactionProvider transactionProvider, IMapper mapper,
            IRecipeService service)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Recipe>> GetById(string id)
        {
            Recipe? entity;
            if (string.IsNullOrWhiteSpace(id) ||
                (entity = await this._service.GetEntityById(id)) == null)
            {
                return BadRequest();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<Recipe>>> GetAll()
        {
            IEnumerable<Recipe> entities = await this._service.GetAll();
            return Ok(entities);
        }

        [HttpGet]
        [Route("execute/{name}")]
        public async Task<ActionResult> ExecuteRecipe(string name)
        {
            return Ok(await _service.ExecuteRecipe(name));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Recipe newEntity)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this

            using var transaction = await this._transactionProvider.BeginTransaction();
            var entity = await this._service.AddEntity(newEntity);
            await transaction.CommitAsync();
            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Recipe update)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this

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