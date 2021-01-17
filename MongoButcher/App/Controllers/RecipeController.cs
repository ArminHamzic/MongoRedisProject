using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Model.Recipe;
using App.Model.Resource;
using AutoMapper;
using LeoMongo.Transaction;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Recipes;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace MongoDBDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecipeService _service;
        private readonly IProductService _productService;
        private readonly ITransactionProvider _transactionProvider;

        public RecipeController(ITransactionProvider transactionProvider, IMapper mapper,
            IRecipeService service, IProductService productService)
        {
            _transactionProvider = transactionProvider;
            _mapper = mapper;
            _service = service;
            _productService = productService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetById(string id)
        {
            Recipe? entity;

            if (string.IsNullOrWhiteSpace(id) ||
                (entity = await this._service.GetEntityById(id)) == null)
            {
                return BadRequest();
            }

            return Ok(new RecipeDTO
            {
                Name = entity.Name,
                Procedure = entity.Procedure,
                Endproduct = entity.Endproduct,
                Incrediants = entity.Incrediants.Select(async i => new ResourceDTO
                    {
                        Product = await _productService.GetByName(i.ProductName),
                        Amount = i.Amount
                    })
                    .Select(task => task.Result)
                    .ToList()
                
        });
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IReadOnlyCollection<RecipeDTO>>> GetAll()
        {
            IEnumerable<Recipe> entities = await this._service.GetAll();
            return Ok(entities);
        }

        [HttpGet]
        [Route("execute/{name}")]
        public async Task<ActionResult> ExecuteRecipe(string name)
        {
            try
            {
                return Ok(await _service.ExecuteRecipe(name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeDTO newEntity)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this

            using var transaction = await this._transactionProvider.BeginTransaction();

            var entity = await this._service.AddEntity(DtoToRecipe(newEntity));
            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetById), new {id = entity.Id.ToString()}, entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(RecipeDTO update)
        {
            // for a real app it would be a good idea to configure model validation to remove long ifs like this

            using var transaction = await this._transactionProvider.BeginTransaction();
            var entity = await this._service.UpdateEntity(DtoToRecipe(update));
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

        private Recipe DtoToRecipe(RecipeDTO recipeDto)
        {
            return new Recipe
            {
                Name = recipeDto.Name,
                Procedure = recipeDto.Procedure,
                Endproduct = recipeDto.Endproduct,
                Incrediants = recipeDto.Incrediants.Select(dto => new Resource
                {
                    Amount = dto.Amount,
                    ActionHistories = dto.ActionHistories,
                    ProductName = dto.Product.Name
                }).ToList()
            };
        }

        private RecipeDTO RecipeToDto(Recipe recipe)
        {
            var ingredients = recipe.Incrediants
                .Select(async resource => new ResourceDTO
                {
                    Amount = resource.Amount,
                    Product = await _productService.GetByName(recipe.Name),
                    ActionHistories = resource.ActionHistories
                }).Select(task => task.Result).ToList();

            return new RecipeDTO
            {
                Name = recipe.Name,
                Procedure = recipe.Procedure,
                Endproduct = recipe.Endproduct,
                Incrediants = ingredients
            };
        }
    }
}