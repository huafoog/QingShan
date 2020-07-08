using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QS.Core.Data;
using QS.Core.Data.Enums;
using QS.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using StatusCodes = QS.Core.Data.Enums.StatusCodes;

namespace QS.Core.Permission
{
    // <summary>
    /// 响应认证处理器
    /// </summary>
    public class ResponseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ResponseAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
            await Response.WriteAsync(JsonConvert.SerializeObject(
                new StatusResult(StatusCodes.Status401Unauthorized.ToDescription()),
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            ));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.ContentType = "application/json";
            Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden;
            await Response.WriteAsync(JsonConvert.SerializeObject(
                 new StatusResult(StatusCodes.Status403Forbidden.ToDescription()),
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            ));
        }
    }

    public class ResponseStatusData
    {
        public StatusCodes Code { get; set; } = StatusCodes.Status1Ok;
        public string Msg { get; set; }
    }
}
