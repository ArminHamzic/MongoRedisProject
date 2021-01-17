using System.Collections.Generic;
using MongoDBDemoApp.Core.Workloads.ActionHistories;
using MongoDBDemoApp.Core.Workloads.Products;

namespace App.Model.Resource
{
    public class ResourceDTO
    {
        public Product Product { get; set; }
        public double Amount { get; set; }
        public List<ActionHistory> ActionHistories { get; set; }
    }
}