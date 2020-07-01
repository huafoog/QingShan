using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Areas.Admin.Controllers.Base
{
    /// <summary>
    /// admin控制器基类
    /// </summary>
    [Area("Admin")]
    [ApiController]
    [Route("/Admin/[controller]/[action]")]
    public class AdminBaseController:ControllerBase
    {

    }
}
