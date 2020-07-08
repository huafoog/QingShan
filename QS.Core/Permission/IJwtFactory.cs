using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace QS.Core.Permission
{
    public interface IJwtFactory
    {
        /// <summary>
        /// 创建用户jwt
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        string Create(Claim[] claims);

        /// <summary>
        /// 获取jwt信息
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        Claim[] Decode(string jwtToken);
    }
}
