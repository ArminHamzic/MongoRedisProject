using System.Threading.Tasks;

namespace MongoDBDemoApp.Core.Workloads.Products
{
    public interface IProductService: IGenericService<Product>
    {
        Task<Product> GetByName(string name);
    }
}