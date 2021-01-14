using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public class RecipeService : GenericService<Recipe>, IRecipeService
    {
        public RecipeService(IDateTimeProvider dateTimeProvider, IRecipeRepository repository,
            ILogger<GenericService<Recipe>> logger) : base(dateTimeProvider, repository, logger)
        {
        }
    }
}