using System.Collections.Generic;
using App.Model.Resource;
using MongoDBDemoApp.Core.Workloads.Products;

namespace App.Model.Recipe
{
    public class RecipeDTO
    {
        public string Name { get; set; }
        public string Procedure { get; set; }
        public Product Endproduct { get; set; }
        public List<ResourceDTO> Incrediants { get; set; }
    }
}