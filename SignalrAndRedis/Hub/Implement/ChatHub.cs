using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalrAndRedis.Hub.Implement
{
    [HubName("ChatHub")]
    public class ChatHub : Hub<IClient>
    {
        private const string ChannelName = "ChatHub";

        /// <summary>傳遞訊息給某人</summary>
        /// <param name="userId">要傳遞的對象</param>
        /// <param name="fromUserName">訊息發送人姓名</param>
        /// <param name="msg">訊息內容</param>
        public void SendPrivateMsg(string userId, string fromUserName, string msg)
        {
            Clients.User(userId).Received($"[{fromUserName}]:{msg} - {DateTime.Now:f}");
        }

        /// <summary>傳送訊息給Hub的所有人</summary>
        /// <param name="fromUserName">訊息發送者姓名</param>
        /// <param name="msg">發送訊息內容</param>
        public void Send(string fromUserName, string msg)
        {
            Clients.All.Received($"[{ChannelName}]{fromUserName}:{msg} - {DateTime.Now:f}");
        }
    }
}