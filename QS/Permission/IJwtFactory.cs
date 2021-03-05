using System.Security.Claims;

namespace QS.Permission
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
