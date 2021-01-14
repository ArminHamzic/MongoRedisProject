using System;
using System.Collections.Generic;
using LeoMongo.Database;
using MongoDBDemoApp.Core.Workloads.Incredients;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Recipes
{
    public class Recipe : EntityBase
    {
        public String Name { get; set; }
        public Product Endproduct { get; set; }
        public List<Incrediant> Incrediants { get; set; }
    }
}