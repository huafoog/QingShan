using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingShan.Data;
using System.Threading.Tasks;
using Template.Services.Demo;
using Template.Services.Demo.Dto;
using Template.Services.Demo.Dto.OutputDto;

namespace Template.Web.Controllers
{
    /// <summary>
    /// Demo 控制器
    /// </summary>
    [ApiDescriptionSettings("Admin")]
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoContract _testContract;

        public DemoController(IDemoContract testContract)
        {
            _testContract = testContract;
        }
        /// <summary>
        /// Demo方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<StatusResult<DemoOutputDto>> Demo(DemoInputDto dto)
        {
            return new StatusResult<DemoOutputDto>(await _testContract.TestAsync());
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<StatusResult> Index()
        {
            return new StatusResult(await _testContract.TestAsync());
        }
    }
}
