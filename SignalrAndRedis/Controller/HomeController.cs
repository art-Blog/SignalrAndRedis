using System.Web.Mvc;
using SignalrAndRedis.Core;
using SignalrAndRedis.Core.Module.Cache;
using SignalrAndRedis.ViewModels.Home;

namespace SignalrAndRedis.Controller
{
    public class HomeController : System.Web.Mvc.Controller
    {
        private readonly ICacheModule _cache = ModuleFactory.GetCacheModule();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DashBoard()
        {
            var model = new HomeDashBoardViewModel
            {
                OnlineUser = _cache.GetOnlineUser()
            };
            return View(model);
        }
    }
}