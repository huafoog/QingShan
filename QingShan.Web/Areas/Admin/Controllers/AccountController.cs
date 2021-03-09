using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingShan.Core.JWT;
using QingShan.Data;
using QingShan.Data.Constants;
using QingShan.Permission;
using QingShan.Services.Account;
using QingShan.Services.Account.Dto;
using QingShan.Services.User;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingShan.Core.Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 用户授权控制器
    /// </summary>
    public class AccountController : AdminBaseController
    {

        private readonly IAccountService _accountService;

        private readonly IJwtFactory _userToken;

        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IJwtFactory userToken, IUserService userService)
        {
            _accountService = accountService;
            _userToken = userToken;
            _userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<object> Test(int id)
        {
            return await _userService.GetAsync(id);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<StatusResult<string>> Login(LoginInputDto dto)
        {
            var result = await _accountService.LoginAsync(dto);
            #region 添加登录日志

            #endregion
            if (!result.IsSuccess)
            {
                return new StatusResult<string>(result.Message);
            }

            // 生成 token
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { ClaimConst.USERID, result.Data.Id },  // 存储Id
                { ClaimConst.USERNAME,result.Data.UserName }, // 存储用户名
                { ClaimConst.USERNICKNAME,result.Data.NickName },
            });

            // 获取刷新 token
            //var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, 30); // 第二个参数是刷新 token 的有效期，默认三十天

            //var token = _userToken.Create(new Claim[] {
            //    new Claim(ClaimConst.USERID,result.Data.Id.ToString()),
            //    new Claim(ClaimConst.USERNAME,result.Data.UserName),
            //    new Claim(ClaimConst.USERNICKNAME,result.Data.NickName)
            //});
            return new StatusResult<string>() { Data = accessToken };
        }
    }
}
