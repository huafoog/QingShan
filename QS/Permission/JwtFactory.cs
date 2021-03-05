using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QS.DependencyInjection;
using QS.Helper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace QS.Permission
{
    public class JwtFactory : IJwtFactory, ITransientDependency
    {

        private readonly IConfiguration _config;

        public JwtFactory(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 创建jwt 授权 token
        /// </summary>
        /// <param name="claims">用户标识</param>
        /// <returns></returns>
        public string Create(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Audience:Secret"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var refreshExpires = DateTime.Now.AddMinutes(_config["Audience:RE"].ToInt()).ToString();
            claims = claims.Append(new Claim(_config["Audience:RE"], refreshExpires)).ToArray();

            var token = new JwtSecurityToken(
                issuer: _config["Audience:Issuer"],
                audience: _config["Audience:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_config["Audience:RE"].ToInt()),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 解析Jwt Token
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <returns></returns>
        public Claim[] Decode(string jwtToken)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            return jwtSecurityToken?.Claims?.ToArray();
        }
    }
}
