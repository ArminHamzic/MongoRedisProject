using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Products
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(IDateTimeProvider dateTimeProvider, IProductRepository repository,
            ILogger<ProductService> logger) : base(dateTimeProvider, repository, logger)
        {
        }
    }
}