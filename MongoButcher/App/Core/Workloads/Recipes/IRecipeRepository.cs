using System.Threading.Tasks;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<Recipe?> GetRecipeByName(string name);
    }
}