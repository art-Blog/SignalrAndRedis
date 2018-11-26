using SignalrAndRedis.Core.Module.Cache;
using SignalrAndRedis.Core.Module.Cache.Implement;
using SignalrAndRedis.Core.Module.SignalR;
using SignalrAndRedis.Core.Module.SignalR.Implement;

namespace SignalrAndRedis.Core
{
    public class ModuleFactory
    {
        public static ICacheModule GetCacheModule()
        {
            return new RedisCache();
        }

        public static IHubEventModule GetHubEventModule()
        {
            return new HubEventModule();
        }
    }
}