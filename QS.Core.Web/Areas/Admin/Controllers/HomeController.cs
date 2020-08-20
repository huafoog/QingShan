using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QS.Core.Attributes.Permission;
using QS.ServiceLayer.ProductService;
using QS.ServiceLayer.ProductService.Dtos;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QS.Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Description("用户管理")]
    public class HomeController: AdminBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IDistributedCache cache)
        {
            _logger = logger;
            _productService = productService;
            _cache = cache;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductOutputDto>), 200)]
        [Description("获取")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("这是Info等级的信息");
            var data = await _productService.Get();
            await _cache.SetStringAsync("str","123456789");
            var res = await _cache.GetStringAsync("str");
            return Ok(res);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加")]
        public async Task<IActionResult> Add(ProductInputDto dto)
        {
            var result = await _productService.Add(dto);
            return Ok(result);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除")]
        public async Task<IActionResult> Delete(IdDto dto)
        {
            var result = await _productService.Delete(dto.Id);
            return Ok(result);
        }
    }
}
