using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingShan.Data;
using System.Threading.Tasks;
using Template.Services.Demo;

namespace Template.Web.Controllers
{
    [ApiDescriptionSettings("Admin")]
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IDemoContract testContract;

        public DemoController(IDemoContract testContract)
        {
            this.testContract = testContract;
        }
        [HttpPost]
        public async Task<StatusResult> Demo()
        {
            return new StatusResult(await testContract.TestAsync());
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<StatusResult> Index()
        {
            return new StatusResult(await testContract.TestAsync());
        }
    }
}
