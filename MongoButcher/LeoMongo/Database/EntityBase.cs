using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace LeoMongo.Database
{
    public abstract class EntityBase
    {
        [BsonId] [JsonIgnore] public ObjectId BaseId { get; set; }

        public string Id => BaseId.ToString();
    }
}