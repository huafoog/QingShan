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
    [Route("api/[Controller]/[Action]")]
    public class TestController:ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly IProductService _productService;
        public TestController(ILogger<TestController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Get()
        {
            var data = await _productService.Get();
            //await _context.AddAsync(new Product() { 
            //    Name = "张飞",
            //    Price = 2.3F
            //});
            //await _context.SaveChangesAsync();
            //_logger.LogInformation("请求");
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputDto dto)
        {
            var result = await _productService.Add(dto);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IdDto dto)
        {
            var result = await _productService.Delete(dto.Id);
            return Ok(result);
        }
    }
}
