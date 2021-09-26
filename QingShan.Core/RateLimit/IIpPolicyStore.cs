using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using QingShan.Cache;
using QingShan.Core.RateLimit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.RateLimit
{
    public class RedisIpPolicyStore : RateLimitStore<IpRateLimitPolicies>, IIpPolicyStore
    {
        private readonly IpRateLimitOptions _options;
        private readonly IpRateLimitPolicies _policies;
        private readonly ICache _cache;

        public RedisIpPolicyStore(
            ICache cache,
            IOptions<MineIpRateLimitOptions> options = null,
            IOptions<IpRateLimitPolicies> policies = null):base(cache)
        {
            _options = options?.Value;
            _policies = policies?.Value;
            _cache = cache;
        }


        public async Task SeedAsync()
        {
            // on startup, save the IP rules defined in appsettings
            if (_options != null && _policies != null)
            {
                await SetAsync($"{_options.IpPolicyPrefix}", _policies).ConfigureAwait(false);
            }
        }
    }
}
