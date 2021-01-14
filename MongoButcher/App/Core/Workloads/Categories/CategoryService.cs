using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Categories
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(IDateTimeProvider dateTimeProvider, ICategoryRepository repository,
            ILogger<GenericService<Category>> logger) : base(dateTimeProvider, repository, logger)
        {
        }
    }
}