using System.Threading.Tasks;

namespace MongoDBDemoApp.Core.Workloads.Resources
{
    public interface IResourceService : IGenericService<Resource>
    {
        Task<Resource?> GetResourceByProductName(string productName);
    }
}