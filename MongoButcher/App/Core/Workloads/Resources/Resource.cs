using System.Collections.Generic;
using LeoMongo.Database;
using MongoDBDemoApp.Core.Workloads.ActionHistories;

namespace MongoDBDemoApp.Core.Workloads.Resources
{
    public class Resource : EntityBase
    {
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public List<ActionHistory> ActionHistories { get; set; }
    }
}