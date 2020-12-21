using System.Collections.Generic;

namespace MongoDBDemoApp.Core.Util
{
    public static class Extensions
    {
        public static string Truncate(this string self, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(self) || self.Length <= maxLength)
            {
                return self;
            }

            return $"{self.Substring(0, maxLength - 3)}...";
        }

        public static (TKey, TValue) ToTuple<TKey, TValue>(this KeyValuePair<TKey, TValue> self) =>
            (self.Key, self.Value);
    }
}