using Microsoft.AspNetCore.Mvc;
using QS.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Areas.Admin.Controllers.Base
{
    /// <summary>
    /// admin控制器基类
    /// </summary>
    [AreaInfo("Admin",Display = "后台管理")]
    [ApiController]
    [Route("/Admin/[controller]/[action]")]
    public class AdminBaseController:ControllerBase
    {

    }
}
