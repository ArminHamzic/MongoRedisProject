using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Resources
{
    public class ResourceService : GenericService<Resource>, IResourceService
    {
        private IResourceRepository _resourceRepository;
        
        public ResourceService(IDateTimeProvider dateTimeProvider, IResourceRepository repository,
            ILogger<GenericService<Resource>> logger) : base(dateTimeProvider, repository, logger)
        {
            _resourceRepository = repository;
        }

        public async Task<Resource?> GetResourceByProductName(string productName)
        {
            return await _resourceRepository.GetResourceByProductName(productName);
        }
    }
}