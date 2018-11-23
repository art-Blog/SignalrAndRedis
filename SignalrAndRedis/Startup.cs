using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalrAndRedis.Startup))]

namespace SignalrAndRedis
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 使用redis scale out
            GlobalHost.DependencyResolver.UseRedis("127.0.0.1", 6380, null, "chat");

            // 如需如何設定應用程式的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}