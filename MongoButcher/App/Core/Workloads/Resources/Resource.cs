using LeoMongo.Database;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Resources
{
    public class Resource : EntityBase
    {
        public Product Product { get; set; }
        public double Amount { get; set; }
    }
}