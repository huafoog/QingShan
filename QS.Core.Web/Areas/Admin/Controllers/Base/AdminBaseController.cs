using Microsoft.AspNetCore.Mvc;
using QS.Core.Attributes;
using QS.Core.Attributes.Permission;

namespace QS.Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// admin控制器基类
    /// </summary>
    [AreaInfo("Admin", Display = "后台管理")]
    [ApiController]
    [Route("/Api/[area]/[controller]/[action]")]
    [Permission]
    public class AdminBaseController : ControllerBase
    {
    }
}
