using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Incredients
{
    public class IncrediantService : GenericService<Incrediant>, IIncrediantService
    {
        public IncrediantService(IDateTimeProvider dateTimeProvider, IIncrediantRepository repository,
            ILogger<GenericService<Incrediant>> logger) : base(dateTimeProvider, repository, logger)
        {
        }
    }
}