using Microsoft.AspNetCore.Authorization;
using QingShan.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingShan.Web.Authorization
{
    /// <summary>
    /// 自定义权限声明
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IUserContract _userContract;

        public PermissionAuthorizationHandler(IUserContract userContract)
        {
            _userContract = userContract;
        }

        /// <summary>
        ///  Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                if (context.User.IsInRole("admin"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var userIdClaim = context.User.FindFirst(_ => _.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim != null)
                    {
                        //if (_userContract.CheckPermission(int.Parse(userIdClaim.Value), requirement.Name))
                        //{
                        //    context.Succeed(requirement);
                        //}
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
