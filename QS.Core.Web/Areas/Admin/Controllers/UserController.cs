using Microsoft.AspNetCore.Mvc;
using QS.Core.Data;
using QS.ServiceLayer.User;
using QS.ServiceLayer.User.Dtos.InputDto;
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
    public class UserController:AdminBaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("新增用户")]
        public async Task<StatusResult> Add(UserAddInputDto input)
        {
            return await _userService.AddAsync(input);
        }
    }
}
