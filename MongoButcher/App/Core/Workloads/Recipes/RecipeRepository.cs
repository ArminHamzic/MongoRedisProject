using System.Threading.Tasks;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }

        public async Task<Recipe?> GetRecipeByName(string name)
        {
            return await Query().FirstOrDefaultAsync(recipe => recipe.Name == name);
        }
    }
}