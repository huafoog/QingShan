using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

namespace QingShan.Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 用户授权控制器
    /// </summary>
    public class AccountController : AdminBaseController
    {

        private readonly IAccountService _accountService;


        private readonly IUserContract _userService;

        public AccountController(IAccountService accountService,
            IUserContract userService)
        {
            _accountService = accountService;
            _userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<object> Test(string id)
        {
            return await _userService.GetAsync(id);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<StatusResult> Init()
        {
            return await _userService.Init();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<StatusResult<string>> UserLogin(LoginInputDto dto)
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
                { ClaimConst.QINGSHANUSERISSUPER,result.Data.IsSuper},
            });
            return new StatusResult<string>() { Data = "Bearer "+accessToken };
        }
    }
}
