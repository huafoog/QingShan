using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using QS.Core.Data;
using QS.Core.Helper;

namespace QS.Core.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHostEnvironment _env;

        public UploadController(IHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public StatusResult<string> UploadFile(IFormFile file) => new StatusResult<string>() { Data = FileHelper.CreateFile(file) };
    }
}