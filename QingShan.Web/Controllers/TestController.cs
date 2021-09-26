using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingShan.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QingShan.Web.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    [ApiDescriptionSettings("Test")]
    public class TestController
    {
        private readonly ICache _cache;

        public TestController(ICache cache)
        {
            _cache = cache;
        }

        public static List<Object> Data { get; set; } = new List<object>();

        [HttpGet]
        public async Task SetData(string name)
        {
            await _cache.SetAsync("key_123",name);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<object>> GetData()
        {
            await Task.CompletedTask;

            return Data;
        }
        [HttpPost]
        public async Task<object> AddData()
        {
            await Task.CompletedTask;
            Data.Add(new
            {
                Id = Data.Count + 1,
                Setup = "What happens to a frog\'s car when it breaks down?",
                Punchline = "It gets toad away",
            });
            return new { 
                code = 200
            };
        }
    }
}
