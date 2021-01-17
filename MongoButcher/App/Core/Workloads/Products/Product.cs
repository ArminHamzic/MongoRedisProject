using LeoMongo.Database;
using MongoDBDemoApp.Core.Workloads.Categories;

namespace MongoDBDemoApp.Core.Workloads.Products
{
    public sealed class Product : EntityBase
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Picture { get; set; }
    }
}
