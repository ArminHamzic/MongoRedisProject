using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Products
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IDateTimeProvider dateTimeProvider, IProductRepository repository,
            ILogger<ProductService> logger) : base(dateTimeProvider, repository, logger)
        {
            _productRepository = repository;
        }

        public async Task<Product> GetByName(string name)
        {
            return await _productRepository.GetByName(name);
        }
    }
}