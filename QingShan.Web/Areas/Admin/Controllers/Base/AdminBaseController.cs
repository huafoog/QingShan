using Microsoft.AspNetCore.Mvc;
using QingShan.Attributes;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// admin控制器基类
    /// </summary>
    [AreaInfo("Admin", Display = "后台管理")]
    [ApiController]
    [Route("/[Area]/[controller]/[action]")]
    [ApiDescriptionSettings("Admin")]
    public class AdminBaseController : ControllerBase
    {
    }
}
