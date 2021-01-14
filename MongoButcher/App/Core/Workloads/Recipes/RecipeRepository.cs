using LeoMongo.Database;
using LeoMongo.Transaction;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }
    }
}