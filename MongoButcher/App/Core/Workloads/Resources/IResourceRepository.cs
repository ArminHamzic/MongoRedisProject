using System.Threading.Tasks;

namespace MongoDBDemoApp.Core.Workloads.Resources
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        Task<Resource?> GetResourceByProductName(string name);
    }
}