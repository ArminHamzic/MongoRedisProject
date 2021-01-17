using System;
using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.Categories
{
    public class Category : EntityBase
    {
        public String Name { get; set; }
        public String Description { get; set; }
    }
}