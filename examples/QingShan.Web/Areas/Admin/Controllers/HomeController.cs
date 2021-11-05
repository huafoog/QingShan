using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using QingShan.Permission;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Description("用户管理")]
    public class HomeController : AdminBaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDistributedCache _cache;

        private readonly IUserInfo _user;

        public HomeController(ILogger<HomeController> logger,
            IDistributedCache cache,
            IUserInfo user)
        {
            _logger = logger;
            _cache = cache;
            _user = user;
        }
    }
}
