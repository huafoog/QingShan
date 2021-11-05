using Microsoft.AspNetCore.Mvc;
using QingShan.CodeGeneratorWeb.CodeGenerator;
using QingShan.CodeGeneratorWeb.CodeGenerator.Dto;
using System.Threading.Tasks;

namespace QingShan.CodeGeneratorWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICodeGenService _renderService;

        public HomeController(ICodeGenService renderService)
        {
            _renderService = renderService;
        }
        [HttpPost]
        [Route("/PostCodegen")]
        public async Task<IActionResult> Post([FromForm] CodegeneratorInputDto model)
        {
            var message = await _renderService.Generator(model);
            ViewBag.Message = message;
            return View("Message");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Message()
        {
            return View();
        }

    }
}
