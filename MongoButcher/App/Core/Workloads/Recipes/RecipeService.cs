using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.ActionHistories;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public class RecipeService : GenericService<Recipe>, IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly IActionHistoryRepository _historyRepository;

        public RecipeService(IDateTimeProvider dateTimeProvider,
            IRecipeRepository repository,
            IResourceRepository resourceRepository,
            IProductRepository productRepository,
            IActionHistoryRepository historyRepository,
            ILogger<GenericService<Recipe>> logger) : base(dateTimeProvider, repository, logger)
        {
            _recipeRepository = repository;
            _resourceRepository = resourceRepository;
            _historyRepository = historyRepository;
        }

        public async Task<Resource> ExecuteRecipe(string recipeName)
        {
            var recipe = await _recipeRepository.GetRecipeByName(recipeName);

            if (recipe == null)
            {
                throw new Exception("Recipe not found: " + recipe);
            }
            
            recipe.Incrediants.ForEach(async incrediant =>
            {
                var resource = await _resourceRepository.GetResourceByProductName(incrediant.Product.Name);

                if (resource == null)
                {
                    throw new Exception("resource of product not found: " + incrediant.Product.Name);
                }

                if (resource.Amount - incrediant.Amount < 0)
                {
                    throw new Exception("To little amount of: " + resource.Product.Name);
                }

                resource.Amount -= incrediant.Amount;

                await _resourceRepository.UpdateEntity(resource);
            });

            var toUpdateResource = await _resourceRepository.GetResourceByProductName(recipe.Endproduct.Name);

            if (toUpdateResource == null)
            {
                throw new Exception("Resource of Endproduct not found: " + recipe.Endproduct.Name);
            }
            
            toUpdateResource.Amount += 1;
            
            return await _resourceRepository.UpdateEntity(toUpdateResource);
        }
    }
}