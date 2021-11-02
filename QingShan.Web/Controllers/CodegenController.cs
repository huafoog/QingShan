using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using QingShan.Core.CodeGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Web.Controllers
{
    [AllowAnonymous]
    public class CodegenController : ControllerBase
    {
        private readonly ICodeGenService _renderService;

        public CodegenController(ICodeGenService renderService)
        {
            _renderService = renderService;
        }
        [HttpGet]
        [Route("/Codegen")]
        public async Task Get()
        {
            var result = _renderService.GetPage("Codegen");
            var data = Encoding.UTF8.GetBytes(result);  
            Response.ContentType = "text/html";
            await Response.Body.WriteAsync(data.AsMemory(0, data.Length));
        }

        [HttpGet]
        [Route("/Test1")]
        public async Task Test()
        {
            await _renderService.Generator();
        }
    }
}
