using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QS.Core.Data;
using QS.Core.Dependency;
using QS.DataLayer.Entities;
using QS.ServiceLayer.Account.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.Account
{
    public class AccountService: IAccountService, IScopeDependency
    {

        private readonly EFContext _context;

        private readonly IConfiguration _config;

        public AccountService(EFContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public StatusResult<string> Login(LoginInputDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Account) && !string.IsNullOrEmpty(dto.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, dto.Account)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Audience:Secret"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _config["Audience:Issuer"],
                    audience: _config["Audience:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return new StatusResult<string>() { Data = new JwtSecurityTokenHandler().WriteToken(token), IsSuccess = true };
            }
            else
            {
                return new StatusResult<string>("请输入账号或密码");
            }
        }
    }
}
