using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDBDemoApp.Core.Util;

namespace MongoDBDemoApp.Core.Workloads.Categories
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryService(IDateTimeProvider dateTimeProvider, ICategoryRepository repository,
            ILogger<GenericService<Category>> logger) : base(dateTimeProvider, repository, logger)
        {
            _categoryRepository = repository;
        }

        public async Task<Category> GetOrCreateCategoryByName(Category requestCategory)
        {
            var category = await _categoryRepository.GetCategoryByName(requestCategory.Name) ??
                           await Repository.AddEntity(requestCategory);

            return category;
        }
    }
}