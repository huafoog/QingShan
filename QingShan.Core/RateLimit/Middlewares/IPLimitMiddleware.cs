using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QingShan.Core.RateLimit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.RateLimit.Middlewares
{
    public class IPLimitMiddleware : IpRateLimitMiddleware
    {
        public IPLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy, IOptions<MineIpRateLimitOptions> options, IRateLimitCounterStore counterStore, IIpPolicyStore policyStore, IRateLimitConfiguration config, ILogger<IPLimitMiddleware> logger)
            : base(next, processingStrategy, options, counterStore, policyStore, config, logger)
        {
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
        }
    }
}
