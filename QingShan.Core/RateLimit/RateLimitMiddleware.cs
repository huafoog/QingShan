using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.RateLimit
{
    public abstract class RateLimitMiddleware<TProcessor> where TProcessor : IRateLimitProcessor
    {
        private readonly RequestDelegate _next;

        private readonly TProcessor _processor;

        private readonly RateLimitOptions _options;

        private readonly IRateLimitConfiguration _config;

        protected RateLimitMiddleware(RequestDelegate next, RateLimitOptions options, TProcessor processor, IRateLimitConfiguration config)
        {
            _next = next;
            _options = options;
            _processor = processor;
            _config = config;
            _config.RegisterResolvers();
        }

        public async Task Invoke(HttpContext context)
        {
            if (_options == null)
            {
                await _next(context);
                return;
            }
            ClientRequestIdentity identity = await ResolveIdentityAsync(context);
            TProcessor processor = _processor;
            if (processor.IsWhitelisted(identity))
            {
                await _next(context);
                return;
            }
            processor = _processor;
            IEnumerable<RateLimitRule> rules = await processor.GetMatchingRulesAsync(identity, context.RequestAborted);
            Dictionary<RateLimitRule, RateLimitCounter> rulesDict = new Dictionary<RateLimitRule, RateLimitCounter>();
            foreach (RateLimitRule rule2 in rules)
            {
                processor = _processor;
                RateLimitCounter rateLimitCounter = await processor.ProcessRequestAsync(identity, rule2, context.RequestAborted);
                if (rule2.Limit > 0.0)
                {
                    if (rateLimitCounter.Timestamp + rule2.PeriodTimespan.Value < DateTime.UtcNow)
                    {
                        continue;
                    }
                    if (rateLimitCounter.Count > rule2.Limit)
                    {
                        string retryAfter = rateLimitCounter.Timestamp.RetryAfterFrom(rule2);
                        LogBlockedRequest(context, identity, rateLimitCounter, rule2);
                        if (_options.RequestBlockedBehaviorAsync != null)
                        {
                            await _options.RequestBlockedBehaviorAsync(context, identity, rateLimitCounter, rule2);
                        }
                        if (!rule2.MonitorMode)
                        {
                            await ReturnQuotaExceededResponse(context, rule2, retryAfter);
                            return;
                        }
                    }
                }
                else
                {
                    LogBlockedRequest(context, identity, rateLimitCounter, rule2);
                    if (_options.RequestBlockedBehaviorAsync != null)
                    {
                        await _options.RequestBlockedBehaviorAsync(context, identity, rateLimitCounter, rule2);
                    }
                    if (!rule2.MonitorMode)
                    {
                        await ReturnQuotaExceededResponse(context, rule2, int.MaxValue.ToString(CultureInfo.InvariantCulture));
                        return;
                    }
                }
                rulesDict.Add(rule2, rateLimitCounter);
            }
            if (rulesDict.Any() && !_options.DisableRateLimitHeaders)
            {
                KeyValuePair<RateLimitRule, RateLimitCounter> rule = rulesDict.OrderByDescending((KeyValuePair<RateLimitRule, RateLimitCounter> x) => x.Key.PeriodTimespan).FirstOrDefault();
                processor = _processor;
                RateLimitHeaders headers = processor.GetRateLimitHeaders(rule.Value, rule.Key, context.RequestAborted);
                headers.Context = context;
                context.Response.OnStarting(new Func<object, Task>(SetRateLimitHeaders), headers);
            }
            await _next(context);
        }

        public virtual async Task<ClientRequestIdentity> ResolveIdentityAsync(HttpContext httpContext)
        {
            string clientIp = null;
            string clientId = null;
            if (_config.ClientResolvers?.Any() ?? false)
            {
                foreach (IClientResolveContributor clientResolver in _config.ClientResolvers)
                {
                    clientId = await clientResolver.ResolveClientAsync(httpContext);
                    if (!string.IsNullOrEmpty(clientId))
                    {
                        break;
                    }
                }
            }
            if (_config.IpResolvers?.Any() ?? false)
            {
                foreach (IIpResolveContributor resolver in _config.IpResolvers)
                {
                    clientIp = resolver.ResolveIp(httpContext);
                    if (!string.IsNullOrEmpty(clientIp))
                    {
                        break;
                    }
                }
            }
            return new ClientRequestIdentity
            {
                ClientIp = clientIp,
                Path = httpContext.Request.Path.ToString().ToLowerInvariant().TrimEnd('/'),
                HttpVerb = httpContext.Request.Method.ToLowerInvariant(),
                ClientId = (clientId ?? "anon")
            };
        }

        public virtual Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            if (rule.QuotaExceededResponse != null)
            {
                _options.QuotaExceededResponse = rule.QuotaExceededResponse;
            }
            string message = string.Format(_options.QuotaExceededResponse?.Content ?? _options.QuotaExceededMessage ?? "API calls quota exceeded! maximum admitted {0} per {1}.", rule.Limit, rule.Period, retryAfter);
            if (!_options.DisableRateLimitHeaders)
            {
                httpContext.Response.Headers["Retry-After"] = retryAfter;
            }
            httpContext.Response.StatusCode = _options.QuotaExceededResponse?.StatusCode ?? _options.HttpStatusCode;
            httpContext.Response.ContentType = _options.QuotaExceededResponse?.ContentType ?? "text/plain";
            return httpContext.Response.WriteAsync(message);
        }

        protected abstract void LogBlockedRequest(HttpContext httpContext, ClientRequestIdentity identity, RateLimitCounter counter, RateLimitRule rule);

        private Task SetRateLimitHeaders(object rateLimitHeaders)
        {
            RateLimitHeaders headers = (RateLimitHeaders)rateLimitHeaders;
            headers.Context.Response.Headers["X-Rate-Limit-Limit"] = headers.Limit;
            headers.Context.Response.Headers["X-Rate-Limit-Remaining"] = headers.Remaining;
            headers.Context.Response.Headers["X-Rate-Limit-Reset"] = headers.Reset;
            return Task.CompletedTask;
        }
    }
}
