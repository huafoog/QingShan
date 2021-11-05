using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.RateLimit
{
    public class IpRateLimitMiddleware : RateLimitMiddleware<IpRateLimitProcessor>
    {
        private readonly ILogger<IpRateLimitMiddleware> _logger;

        public IpRateLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy, Microsoft.Extensions.Options.IOptions<IpRateLimitOptions> options, IRateLimitCounterStore counterStore, IIpPolicyStore policyStore, IRateLimitConfiguration config, ILogger<IpRateLimitMiddleware> logger)
            : base(next, (RateLimitOptions)(options?.Value), new IpRateLimitProcessor(options?.Value, counterStore, policyStore, config, processingStrategy), config)
        {
            _logger = logger;
        }

        protected override void LogBlockedRequest(HttpContext httpContext, ClientRequestIdentity identity, RateLimitCounter counter, RateLimitRule rule)
        {
            _logger.LogInformation($"Request {identity.HttpVerb}:{identity.Path} from IP {identity.ClientIp} has been blocked, quota {rule.Limit}/{rule.Period} exceeded by {counter.Count - rule.Limit}. Blocked by rule {rule.Endpoint}, TraceIdentifier {httpContext.TraceIdentifier}. MonitorMode: {rule.MonitorMode}");
        }
    }
}
