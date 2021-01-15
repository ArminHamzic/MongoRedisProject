using System;
using System.Collections.Generic;
using LeoMongo.Database;
using MongoDBDemoApp.Core.Workloads.Products;
using MongoDBDemoApp.Core.Workloads.Resources;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public class Recipe : EntityBase
    {
        public String Name { get; set; }
        public String Procedure { get; set; }
        public Product Endproduct { get; set; }
        public List<Resource> Incrediants { get; set; }
    }
}