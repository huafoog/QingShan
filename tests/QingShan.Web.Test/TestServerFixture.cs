using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace QingShan.Web.Test
{
    public class TestServerFixture: IDisposable
    {
        private TestServer _testServer;
        public HttpClient Client { get; set; }

        public TestServerFixture()
        {
           
        }
        [Fact]
        public void Test()
        {
            var bulild = new WebHostBuilder().UseStartup<OptionStartup>();
            _testServer = new TestServer(bulild);
            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}
