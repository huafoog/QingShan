using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        public IPLimitMiddleware(RequestDelegate next, AspNetCoreRateLimit.IProcessingStrategy processingStrategy, IOptions<MineIpRateLimitOptions> options, AspNetCoreRateLimit.IRateLimitCounterStore counterStore, AspNetCoreRateLimit.IIpPolicyStore policyStore, AspNetCoreRateLimit.IRateLimitConfiguration config, ILogger<IPLimitMiddleware> logger)
            : base(next, processingStrategy, options, counterStore, policyStore, config, logger)
        {
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, AspNetCoreRateLimit.RateLimitRule rule, string retryAfter)
        {
            httpContext.Response.Headers.Append("Access-Control-Allow-Origin", "*");


            //string str = string.Format("API calls quata exceeded! maximum maximum admitted {0} per {1}", rule. ,
            //    rule.Period);
            //var result = JsonConvert.SerializeObject(new { error = str });
            //httpContext.Response.Headers["Retry-After"] = retryAfter;
            //httpContext.Response.StatusCode = 429;
            //httpContext.Response.ContentType = "application/json";

            return base.ReturnQuotaExceededResponse(httpContext, rule, retryAfter);
        }


        protected override void LogBlockedRequest(HttpContext httpContext, AspNetCoreRateLimit.ClientRequestIdentity identity, AspNetCoreRateLimit.RateLimitCounter counter, AspNetCoreRateLimit.RateLimitRule rule)
        {
            base.LogBlockedRequest(httpContext, identity, counter, rule);
        }
    }
}
