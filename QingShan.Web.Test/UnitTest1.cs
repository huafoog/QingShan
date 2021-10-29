using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace QingShan.Web.Test
{
    public class UnitTest1 : IClassFixture<ApplicationFactory<OptionStartup>>
    {
        private readonly ApplicationFactory<OptionStartup> _factory;
        protected readonly ITestOutputHelper Output;
        public UnitTest1(ApplicationFactory<OptionStartup> factory, ITestOutputHelper output)
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
