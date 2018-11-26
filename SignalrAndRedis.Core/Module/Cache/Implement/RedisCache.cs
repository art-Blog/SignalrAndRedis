using Newtonsoft.Json;
using SignalrAndRedis.Core.Module.Cache.Config;
using SignalrAndRedis.Entity.DTO;
using StackExchange.Redis;

namespace SignalrAndRedis.Core.Module.Cache.Implement
{
    internal class RedisCache : ICacheModule
    {
        private static readonly ConnectionMultiplexer Conn
            = CreateRedisConnection(CacheServer.Host, CacheServer.Port);

        private readonly IDatabase _db = Conn.GetDatabase();

        private string FindCache(string key) => _db.StringGet(key);

        private bool IsExistCache(string key) => _db.KeyExists(key);

        private void RemoveCache(string key) => _db.KeyDelete(key);

        private void UpdateCache(string key, object obj)
            => _db.StringSet(key, JsonConvert.SerializeObject(obj));

        private static ConnectionMultiplexer CreateRedisConnection(string server, int port)
            => ConnectionMultiplexer.Connect($"{server}:{port}");

        public UserCache GetUserById(string id)
        {
            var key = $"user:{id}";
            return IsExistCache(key)
                ? JsonConvert.DeserializeObject<UserCache>(FindCache(key))
                : new UserCache(id);
        }

        public void UpdateUserCache(string id, UserCache obj)
            => UpdateCacheObject($"user:{id}", obj, obj.ConnectIds.Count);

        public void UpdateOnlineUser(OnlineUserCache obj)
            => UpdateCacheObject("onlineUser", obj, obj.List.Count);

        public OnlineUserCache GetOnlineUser()
        {
            var key = "onlineUser";
            return IsExistCache(key)
                ? JsonConvert.DeserializeObject<OnlineUserCache>(FindCache(key))
                : new OnlineUserCache();
        }

        private void UpdateCacheObject(string key, object obj, int count)
        {
            if (count == 0)
                RemoveCache(key);
            else
                UpdateCache(key, obj);
        }
    }
}