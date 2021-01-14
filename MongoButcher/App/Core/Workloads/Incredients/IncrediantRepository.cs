using LeoMongo.Database;
using LeoMongo.Transaction;

namespace MongoDBDemoApp.Core.Workloads.Incredients
{
    public class IncrediantRepository : GenericRepository<Incrediant>, IIncrediantRepository
    {
        public IncrediantRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) :
            base(transactionProvider, databaseProvider)
        {
        }
    }
}