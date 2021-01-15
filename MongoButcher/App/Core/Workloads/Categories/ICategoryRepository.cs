using System.Threading.Tasks;

namespace MongoDBDemoApp.Core.Workloads.Categories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> GetCategoryByName(string name);
    }
}