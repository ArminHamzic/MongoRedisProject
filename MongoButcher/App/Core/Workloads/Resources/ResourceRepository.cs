using System.Threading.Tasks;
using LeoMongo.Database;
using LeoMongo.Transaction;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace MongoDBDemoApp.Core.Workloads.Resources
{
    public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) : base(
            transactionProvider, databaseProvider)
        {
        }

        public async Task<Resource?> GetResourceByProductName(string name)
        {
            return await Query().FirstOrDefaultAsync(resource => resource.Product.Name == name);
        }
    }
}