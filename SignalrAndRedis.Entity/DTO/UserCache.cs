using System.Collections.Generic;
using System.Linq;

namespace SignalrAndRedis.Entity.DTO
{
    public class UserCache
    {
        public string UserId { get; }
        public IList<ConnectInfo> ConnectIds { get; set; }

        public UserCache(string id)
        {
            UserId = id;
            ConnectIds = new List<ConnectInfo>();
        }

        public void AddConnectInfo(ConnectInfo info)
        {
            var currectInfo = ConnectIds.FirstOrDefault(x => x.ConnectId == info.ConnectId);
            if (currectInfo != null)
            {
                currectInfo.ConnectBeginTime = info.ConnectBeginTime;
            }

            ConnectIds.Add(info);
        }

        public void RemoveConnectInfo(string connectionId)
        {
            var info = ConnectIds.FirstOrDefault(x => x.ConnectId == connectionId);
            if (info == null) return;

            ConnectIds.Remove(info);
        }
    }
}