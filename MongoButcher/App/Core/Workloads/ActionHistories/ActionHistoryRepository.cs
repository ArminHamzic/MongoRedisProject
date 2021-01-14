using LeoMongo.Database;
using LeoMongo.Transaction;

namespace MongoDBDemoApp.Core.Workloads.ActionHistories
{
    public class ActionHistoryRepository : GenericRepository<ActionHistory>, IActionHistoryRepository
    {
        public ActionHistoryRepository(ITransactionProvider transactionProvider, IDatabaseProvider databaseProvider) :
            base(transactionProvider, databaseProvider)
        {
        }
    }
}