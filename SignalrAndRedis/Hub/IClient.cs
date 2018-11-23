using System.Threading.Tasks;

namespace SignalrAndRedis.Hub
{
    public interface IClient
    {
        Task Received(string message);
    }
}