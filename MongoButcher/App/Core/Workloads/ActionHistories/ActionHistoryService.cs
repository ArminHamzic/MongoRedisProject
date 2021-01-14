using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Categories;

namespace MongoDBDemoApp.Core.Workloads.ActionHistories
{
    public class ActionHistoryService : GenericService<ActionHistory>, IActionHistoryService
    {
        public ActionHistoryService(IDateTimeProvider dateTimeProvider, IActionHistoryRepository repository,
            ILogger<GenericService<ActionHistory>> logger) : base(dateTimeProvider, repository, logger)
        {
        }
    }
}