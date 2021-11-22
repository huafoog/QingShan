using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Services.Demo
{
    public class DemoService : IDemoContract,IScopeDependency
    {
        public async Task<string> TestAsync()
        {
            await Task.CompletedTask;
            return "测试";
        }
    }
}
