using System;
using LeoMongo.Database;

namespace MongoDBDemoApp.Core.Workloads.ActionHistories
{
    public class ActionHistory : EntityBase
    {
        public String Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}