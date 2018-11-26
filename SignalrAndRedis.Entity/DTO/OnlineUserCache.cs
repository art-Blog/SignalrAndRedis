using System.Collections.Generic;
using System.Linq;

namespace SignalrAndRedis.Entity.DTO
{
    public class OnlineUserCache
    {
        public int Count => List.Count;
        public List<UserCacheInfo> List { get; set; } = new List<UserCacheInfo>();

        public void Add(string id)
        {
            var item = List.FirstOrDefault(x => x.UserId == id);
            if (item == null)
            {
                List.Add(new UserCacheInfo { UserId = id, ConnectionIdsCount = 1 });
            }
            else
            {
                item.ConnectionIdsCount += 1;
            }
        }

        public void Remove(string id)
        {
            var item = List.FirstOrDefault(x => x.UserId == id);
            if (item == null) return;

            if (item.ConnectionIdsCount == 1)
            {
                List.Remove(item);
            }
            else
            {
                item.ConnectionIdsCount -= 1;
            }
        }
    }

    public class UserCacheInfo
    {
        public string UserId { get; set; }
        public int ConnectionIdsCount { get; set; }
    }
}