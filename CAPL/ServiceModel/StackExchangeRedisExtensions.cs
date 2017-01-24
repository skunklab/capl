

namespace Capl.ServiceModel
{
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading.Tasks;

    public static class StackExchangeRedisExtensions
    {
        public static T Get<T>(this IDatabase cache, string key)
        {
            byte[] stream = (byte[])cache.StringGet(key);
            return Deserialize<T>(stream);
        }

        /// <summary>
        /// Gets a key from the database and returns an object; normativel T is "object"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDatabase cache, string key)
        {
            return Deserialize<T>(await cache.StringGetAsync(key));
        }

        public static T[] GetSortedSet<T>(this IDatabase cache, string key)
        {
            long length = cache.SortedSetLength(key);
            RedisValue[] values = cache.SortedSetRangeByScore(key);

            List<T> list = new List<T>();
            foreach (RedisValue rv in values)
            {
                list.Add(Deserialize<T>(rv));
            }

            return list.ToArray();
        }

        public static void Set(this IDatabase cache, string key, object value)
        {
            byte[] serializedValue = Serialize(value);
            cache.StringSet(key, serializedValue);
        }

        public static async Task SetAsync(this IDatabase cache, string key, object value)
        {
            byte[] serializedValue = Serialize(value);
            await cache.StringSetAsync(key, serializedValue);
        }


        public static void SetSortSet(this IDatabase cache, string key, object value, double score)
        {
            byte[] serializedValue = Serialize(value);
            cache.SortedSetAdd(key, serializedValue, score);
        }

        public static async Task SetSortSetAsync(this IDatabase cache, string key, object value, double score)
        {
            byte[] serializedValue = Serialize(value);
            await cache.SortedSetAddAsync(key, serializedValue, score);
        }

        public static void DeleteFromSortSet<T>(this IDatabase cache, string key, T value)
        {
            byte[] serializedValue = Serialize(value);          
            cache.SortedSetRemove(key, serializedValue);
        }

        public static async Task DeleteFromSortSetAsync<T>(this IDatabase cache, string key, T value)
        {
            byte[] serializedValue = Serialize(value);
            await cache.SortedSetRemoveAsync(key, serializedValue);
        }

        static byte[] Serialize(object o)
        {
            if (o == null)
            {
                return null;
            }

            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, o);
                    byte[] objectDataAsStream = memoryStream.ToArray();
                    return objectDataAsStream;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("Redis serialation failed.");
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        static T Deserialize<T>(byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (MemoryStream memoryStream = new MemoryStream(stream))
            {
                T result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }
    }
}
