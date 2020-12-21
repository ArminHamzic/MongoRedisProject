using LeoMongo.Database;

namespace LeoMongo
{
    public static class MongoUtil
    {
        public static string GetCollectionName<T>() where T : EntityBase
        {
            string? name = typeof(T).Name;
            return name.FirstLetterLowerCase();
        }
    }
}