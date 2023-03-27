using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QingShan.Core.FreeSql.Options;
using System.Threading.Tasks;
using Free = FreeSql;
using System.Linq;
using HuaFoog.CodeGenerator.CodeGenerator;
using HuaFoog.CodeGenerator.CodeGenerator.Dto;

namespace QingShan.CodeGeneratorWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICodeGenService _renderService;
        private readonly IFreeSql _freeSql;
        private readonly IOptions<DatabaseAccessorSettingsOptions> _dbConfig;

        public HomeController(ICodeGenService renderService,IFreeSql freeSql,IOptions<QingShan.Core.FreeSql.Options.DatabaseAccessorSettingsOptions> dbConfig)
        {
            _renderService = renderService;
            _freeSql = freeSql;
            _dbConfig = dbConfig;
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
            var db = _freeSql.DbFirst.GetTablesByDatabase(_dbConfig.Value.Database).Where(o=>o.Type == Free.DatabaseModel.DbTableType.TABLE);


            ViewBag.TableList = db.Select(o => o.Name).ToArray();
            return View();
        }
        public IActionResult Message()
        {
            return View();
        }

    }
}
