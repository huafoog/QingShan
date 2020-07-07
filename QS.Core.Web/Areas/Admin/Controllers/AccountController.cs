using Microsoft.AspNetCore.Mvc;
using QS.Core.Data;
using QS.ServiceLayer.Account;
using QS.ServiceLayer.Account.Dto;
using QS.ServiceLayer.Account.Dto.OutputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Areas.Admin.Controllers
{

    /// <summary>
    /// 用户授权控制器
    /// </summary>
    public class AccountController:AdminBaseController
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResponseOutput<AuthLoginOutputDto>> Login(LoginInputDto dto)
        {
            var result = await _accountService.LoginAsync(dto);
            #region 添加登录日志

            #endregion
            if (!result.IsSuccess)
            {
                return result;
            }

            return result;
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
