using System.Threading.Tasks;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Driver.Linq;

namespace MongoDBDemoApp.Core.Workloads.Products
{
    public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }

        public Task<Product> GetByName(string name)
        {
            return Query().FirstOrDefaultAsync(product => product.Name == name);
        }
    }
}