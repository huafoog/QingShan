using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QS.ServiceLayer.ProductService;
using QS.ServiceLayer.ProductService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class TestController:ControllerBase
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            throw new Exception("出现错误");
        }
        
    }
}
