using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QS.Core.Data;
using QS.Core.Data.Constants;
using QS.Core.Permission;
using QS.ServiceLayer.Account;
using QS.ServiceLayer.Account.Dto;
using QS.ServiceLayer.Account.Dto.OutputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QS.Core.Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 用户授权控制器
    /// </summary>
    public class AccountController:AdminBaseController
    {

        private readonly IAccountService _accountService;

        private readonly IJwtFactory _userToken;

        public AccountController(IAccountService accountService,IJwtFactory userToken)
        {
            _accountService = accountService;
            _userToken = userToken;
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<StatusResult> Login(LoginInputDto dto)
        {
            //var result = await _accountService.LoginAsync(dto);
            var result = new StatusResult<AuthLoginOutputDto>()
            {
                IsSuccess = true,
                Data = new AuthLoginOutputDto()
                {
                    Id = 1,
                    NickName = "123",
                    UserName = "345"
                }
            };
            #region 添加登录日志

            #endregion
            if (!result.IsSuccess) return new StatusResult(result.Message);
            var token = _userToken.Create(new Claim[] { 
                new Claim(ClaimConst.USERID,result.Data.Id.ToString()),
                new Claim(ClaimConst.USERNAME,result.Data.UserName),
                new Claim(ClaimConst.USERNICKNAME,result.Data.NickName)
            });
            await Task.CompletedTask;
            return new StatusResult(token);
        }

        ///// <summary>
        ///// 获得token
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //private IResponseOutput GetToken(StatusResult<AuthLoginOutputDto> dto)
        //{
        //    if (!dto.IsSuccess)
        //    {
        //        return (IResponseOutput)dto;
        //    }

        //    var user = dto.Data;
        //    var token = _userToken.Create(new[]
        //    {
        //        new Claim(ClaimAttributes.UserId, user.Id.ToString()),
        //        new Claim(ClaimAttributes.UserName, user.UserName),
        //        new Claim(ClaimAttributes.UserNickName, user.NickName)
        //    });

        //    return ResponseOutput.Ok(new { token });
        //}
    }
}
