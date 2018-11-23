using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalrAndRedis.Hub.Implement
{
    [HubName("ChatHub")]
    public class ChatHub : Hub<IClient>
    {
        private const string ChannelName = "ChatHub";

        /// <summary>�ǻ��T�����Y�H</summary>
        /// <param name="userId">�n�ǻ�����H</param>
        /// <param name="fromUserName">�T���o�e�H�m�W</param>
        /// <param name="msg">�T�����e</param>
        public void SendPrivateMsg(string userId, string fromUserName, string msg)
        {
            Clients.User(userId).Received($"[{fromUserName}]:{msg} - {DateTime.Now:f}");
        }

        /// <summary>�ǰe�T����Hub���Ҧ��H</summary>
        /// <param name="fromUserName">�T���o�e�̩m�W</param>
        /// <param name="msg">�o�e�T�����e</param>
        public void Send(string fromUserName, string msg)
        {
            Clients.All.Received($"[{ChannelName}]{fromUserName}:{msg} - {DateTime.Now:f}");
        }
    }
}