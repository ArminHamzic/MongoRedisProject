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
            var options = new CreateIndexOptions() { Unique = true };
            var field = new StringFieldDefinition<Resource>("ProductName");
            
            var indexDefinition = new IndexKeysDefinitionBuilder<Resource>().Ascending(field);
            var indexModel = new CreateIndexModel<Resource>(indexDefinition,options);
            
            GetCollection<Resource>("resource").Indexes.CreateOneAsync(indexModel);
        }

        public async Task<Resource?> GetResourceByProductName(string name)
        {
            return await Query().FirstOrDefaultAsync(resource => resource.ProductName == name);
        }
    }
}