using SignalrAndRedis.Entity.DTO;

namespace SignalrAndRedis.Core.Module.Cache
{
    public interface ICacheModule
    {
        UserCache GetUserById(string id);

        void UpdateUserCache(string id, UserCache user);

        OnlineUserCache GetOnlineUser();

        void UpdateOnlineUser(OnlineUserCache onlineUser);
    }
}