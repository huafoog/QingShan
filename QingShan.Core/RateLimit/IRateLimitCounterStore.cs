using AspNetCoreRateLimit;
using QingShan.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.RateLimit
{
    public class RedisCounterStore : RateLimitStore<RateLimitCounter?>, IRateLimitCounterStore
    {
        public RedisCounterStore(ICache cache):base(cache)
        {
        }
    }
}
   