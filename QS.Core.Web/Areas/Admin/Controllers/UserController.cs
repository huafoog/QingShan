using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QS.Core.Data;
using QS.Core.Permission;
using QS.Core.Permission.Authorization;
using QS.Core.Web.Filter.Transaction;
using QS.ServiceLayer.User;
using QS.ServiceLayer.User.Dtos.InputDto;
using QS.ServiceLayer.User.Dtos.OutputDto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QS.Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Description("用户管理")]
    [ModuleInfo(URL = "/Admin/Url/Index",Module = Data.Enums.ModuleEnum.System,Sort = 0)]
    public class UserController:AdminBaseController
    {
        private readonly IUserService _userService;
        public readonly IUserInfo _userInfo;
        public UserController(IUserService userService, IUserInfo userInfo)
        {
            _userService = userService;
            _userInfo = userInfo;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("新增用户")]
        [ModuleInfo]
        [TransactionInterceptor]
        public async Task<StatusResult> Add(UserAddInputDto input)
        {
            return await _userService.AddAsync(input);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取用户信息")]
        [ModuleInfo]
        public async Task<StatusResult<UserGetOutputDto>> GetUserInfo()
        {
            return await _userService.GetAsync(_userInfo.Id);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取用户信息")]
        [ModuleInfo]
        public async Task<PageOutputDto<UserListOutputDto>> GetUserPage([FromQuery]PageInputDto dto)
        {
            return await _userService.PageAsync(dto);
        }
    }
}
