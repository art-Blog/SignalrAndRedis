using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalrAndRedis.Core;
using SignalrAndRedis.Core.Module.Cache;
using SignalrAndRedis.Core.Module.SignalR;

namespace SignalrAndRedis.Hub.Implement
{
    [HubName("ChatHub")]
    public class ChatHub : Hub<IClient>
    {
        private const string ChannelName = "ChatHub";
        private readonly IHubEventModule _hubEvent = ModuleFactory.GetHubEventModule();
        private readonly ICacheModule _cache = ModuleFactory.GetCacheModule();

        /// <summary>傳遞訊息給某人</summary>
        /// <param name="userId">要傳遞的對象</param>
        /// <param name="fromUserName">訊息發送人姓名</param>
        /// <param name="msg">訊息內容</param>
        public void SendPrivateMsg(string userId, string fromUserName, string msg)
        {
            var userInfo = _cache.GetUserById(userId);
            foreach (var connectInfo in userInfo.ConnectIds)
            {
                var message = $"[{fromUserName}]:{msg} - {DateTime.Now:f}";
                Clients.Client(connectInfo.ConnectId).Received(message);
            }
        }

        /// <summary>傳送訊息給Hub的所有人</summary>
        /// <param name="fromUserName">訊息發送者姓名</param>
        /// <param name="msg">發送訊息內容</param>
        public void Send(string fromUserName, string msg)
        {
            Clients.All.Received($"[{ChannelName}]{fromUserName}:{msg} - {DateTime.Now:f}");
        }

        public override Task OnConnected()
        {
            var id = Context.QueryString["id"];
            _hubEvent.OnConnected(id, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var id = Context.QueryString["id"];
            _hubEvent.OnDisconnected(id, Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var id = Context.QueryString["id"];
            _hubEvent.OnReconnected(id, Context.ConnectionId);
            return base.OnReconnected();
        }
    }
}