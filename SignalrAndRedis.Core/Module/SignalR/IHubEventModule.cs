namespace SignalrAndRedis.Core.Module.SignalR
{
    public interface IHubEventModule
    {
        void OnConnected(string id, string connectionId);

        void OnDisconnected(string id, string connectionId);

        void OnReconnected(string id, string connectionId);
    }
}