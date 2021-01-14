using System;
using LeoMongo.Database;
using MongoDB.Bson.Serialization.Options;
using MongoDBDemoApp.Core.Workloads.Products;

namespace MongoDBDemoApp.Core.Workloads.Incredients
{
    public class Incrediant : EntityBase
    {
        public Product Product { get; set; }
        public double Amount { get; set; }
        public String Unit { get; set; }
    }
}