using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingShan.Cache;
using QingShan.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QingShan.Core.FreeSql.UnitOfWork.Attributes;
using QingShan.Data;

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
        private readonly IUserContract _userContract;

        public TestController(ICache cache, IUserContract userContract)
        {
            _cache = cache;
            _userContract = userContract;
        }

        public static List<Object> Data { get; set; } = new List<object>();

        [HttpGet]
        public async Task SetData(string name)
        {
            await _cache.SetAsync("key_123",name);
        }
        [HttpGet]
        [Transaction]
        public async Task Test()
        {
            await _userContract.GetAsync("123");
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

        [HttpPut]
        public async Task<StatusResult> GetAsync([FromBody] SysUser entity)
        {
            await Task.CompletedTask;

            return new StatusResult();
        }
    }
}
