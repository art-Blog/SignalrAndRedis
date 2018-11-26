using System;
using SignalrAndRedis.Core.Module.Cache;
using SignalrAndRedis.Entity.DTO;

namespace SignalrAndRedis.Core.Module.SignalR.Implement
{
    public class HubEventModule : IHubEventModule
    {
        private readonly ICacheModule _cache = ModuleFactory.GetCacheModule();

        public void OnConnected(string id, string connectionId)
        {
            // 處理個人資料
            var user = _cache.GetUserById(id);
            var info = new ConnectInfo { ConnectId = connectionId, ConnectBeginTime = DateTime.Now };
            user.AddConnectInfo(info);
            _cache.UpdateUserCache(id, user);

            // 處理在線人數
            var onlineUser = _cache.GetOnlineUser();
            onlineUser.Add(id);
            _cache.UpdateOnlineUser(onlineUser);
        }

        public void OnDisconnected(string id, string connectionId)
        {
            // 處理個人資料
            var user = _cache.GetUserById(id);
            user.RemoveConnectInfo(connectionId);
            _cache.UpdateUserCache(id, user);

            // 處理在線人數
            var onlineUser = _cache.GetOnlineUser();
            onlineUser.Remove(id);
            _cache.UpdateOnlineUser(onlineUser);
        }

        public void OnReconnected(string id, string connectionId)
        {
            // 處理個人資料
            var user = _cache.GetUserById(id);
            var info = new ConnectInfo { ConnectId = connectionId, ConnectBeginTime = DateTime.Now };
            user.AddConnectInfo(info);
            _cache.UpdateUserCache(id, user);

            // 處理在線人數
            var onlineUser = _cache.GetOnlineUser();
            onlineUser.Add(id);
            _cache.UpdateOnlineUser(onlineUser);
        }
    }
}