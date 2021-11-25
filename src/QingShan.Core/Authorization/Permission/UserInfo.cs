using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using QingShan.Data.Constants;
using QingShan.DependencyInjection;
using QingShan.Utilities;
using System;
using System.Linq;
using System.Security.Claims;

namespace QingShan.Permission
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public class UserInfo : IUserInfo
    {
        private readonly IHttpContextAccessor _accessor;


        public UserInfo(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id
        {
            get
            {
                var id = _accessor.HttpContext.User.FindFirstValue(ClaimConst.USERID);
                return id;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name
        {
            get
            {
                var name = _accessor?.HttpContext?.User?.FindFirstValue(ClaimConst.USERNAME);

                return name;
            }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get
            {
                var name = _accessor?.HttpContext?.User?.FindFirstValue(ClaimConst.USERNICKNAME);

                return name;
            }
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuper
        {
            get
            {
                var isSuper = _accessor?.HttpContext?.User?.FindFirstValue(ClaimConst.QINGSHANUSERISSUPER);
                bool.TryParse(isSuper,out bool super);
                return super;
            }
        }

    }
}
