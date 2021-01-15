using System.Threading.Tasks;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public interface IRecipeService : IGenericService<Recipe>
    {
        Task<Resource> ExecuteRecipe(string recipeName);
    }
}