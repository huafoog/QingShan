using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace QingShan.Web.Test
{
    public class OptionTest : IClassFixture<ApplicationFactory<OptionStartup>>
    {
        private readonly ApplicationFactory<OptionStartup> _factory;
        protected readonly ITestOutputHelper Output;
        public OptionTest(ApplicationFactory<OptionStartup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            Output = output;
        }


        [Fact]
        public void TestMethod1()
        {
            Output.WriteLine("hello world");
        }
    }
}
