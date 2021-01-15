using System.Threading.Tasks;

namespace MongoDBDemoApp.Core.Workloads.Categories
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<Category> GetOrCreateCategoryByName(Category requestCategory);
    }
}