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

        /// <summary>�ǻ��T�����Y�H</summary>
        /// <param name="userId">�n�ǻ�����H</param>
        /// <param name="fromUserName">�T���o�e�H�m�W</param>
        /// <param name="msg">�T�����e</param>
        public void SendPrivateMsg(string userId, string fromUserName, string msg)
        {
            var userInfo = _cache.GetUserById(userId);
            foreach (var connectInfo in userInfo.ConnectIds)
            {
                var message = $"[{fromUserName}]:{msg} - {DateTime.Now:f}";
                Clients.Client(connectInfo.ConnectId).Received(message);
            }
        }

        /// <summary>�ǰe�T����Hub���Ҧ��H</summary>
        /// <param name="fromUserName">�T���o�e�̩m�W</param>
        /// <param name="msg">�o�e�T�����e</param>
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