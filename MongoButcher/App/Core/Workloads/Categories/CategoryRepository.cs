using System.Threading.Tasks;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Categories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            return await Query().FirstOrDefaultAsync(category => category.Name == name);
        }
    }
}