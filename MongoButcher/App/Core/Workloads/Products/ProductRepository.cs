using LeoMongo.Database;
using LeoMongo.Transaction;

namespace MongoDBDemoApp.Core.Workloads.Products
{
    public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }
    }
}