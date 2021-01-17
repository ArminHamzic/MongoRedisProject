using System.Threading.Tasks;

namespace MongoDBDemoApp.Core.Workloads.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByName(string name);
    }
}