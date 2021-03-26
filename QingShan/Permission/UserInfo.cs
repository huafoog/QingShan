using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using QingShan.Data.Constants;
using QingShan.DependencyInjection;
using QingShan.Utilities;
using System;

namespace QingShan.Permission
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public class UserInfo : IUserInfo, ITransientDependency
    {
        private readonly IHttpContextAccessor _accessor;

        private readonly IConfiguration _config;

        public UserInfo(IHttpContextAccessor accessor, IConfiguration config)
        {
            _accessor = accessor;
            _config = config;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id
        {
            get
            {
                var id = _accessor?.HttpContext?.User?.FindFirst(ClaimConst.USERID);
                if (id != null && id.Value.NotNull())
                {
                    return id.Value.ToInt();
                }
                return 0;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name
        {
            get
            {
                var name = _accessor?.HttpContext?.User?.FindFirst(ClaimConst.USERNAME);

                if (name != null && name.Value.NotNull())
                {
                    return name.Value;
                }

                return "";
            }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get
            {
                var name = _accessor?.HttpContext?.User?.FindFirst(ClaimConst.USERNICKNAME);

                if (name != null && name.Value.NotNull())
                {
                    return name.Value;
                }

                return "";
            }
        }
    }
}
