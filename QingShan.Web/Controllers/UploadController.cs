using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using QingShan.Data;
using QingShan.Utilities;

namespace QingShan.Core.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiDescriptionSettings("Upload")]
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
        [AllowAnonymous]
        public StatusResult<string> UploadFile(IFormFile file)
        {
            return new StatusResult<string>() { Data = FileHelper.CreateFile(file) };
        }
    }
}