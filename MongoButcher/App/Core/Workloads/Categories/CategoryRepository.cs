using LeoMongo.Database;
using LeoMongo.Transaction;

namespace MongoDBDemoApp.Core.Workloads.Categories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }
    }
}