using Microsoft.AspNetCore.Mvc;
using QingShan.Core.FreeSql.UnitOfWork.TransactionInterceptor;
using QingShan.Data;
using QingShan.DatabaseAccessor;
using QingShan.Permission;
using QingShan.Permission.Authorization;
using QingShan.Services.User;
using QingShan.Services.User.Dtos.InputDto;
using QingShan.Services.User.Dtos.OutputDto;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Description("用户管理")]
    [ModuleInfo(URL = "/Admin/Url/Index", Module = Data.Enums.ModuleEnum.System, Sort = 0)]
    public class UserController : AdminBaseController
    {
        private readonly IUserContract _userService;
        public readonly IUserInfo _userInfo;
        public UserController(IUserContract userService, IUserInfo userInfo)
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
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改用户")]
        [ModuleInfo]
        public async Task<StatusResult> Update(UserUpdateInputDto input)
        {
            return await _userService.UpdateAsync(input);
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取当前登录用户信息")]
        [ModuleInfo]
        public async Task<StatusResult<UserGetOutputDto>> GetUserInfo()
        {
            return await _userService.GetAsync(_userInfo.Id);
        }

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取用户详情信息")]
        [ModuleInfo]
        public async Task<StatusResult<UserGetOutputDto>> GetInfo(CommonIdInputDto dto)
        {
            return await _userService.GetAsync(dto.Id);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取用户信息")]
        [ModuleInfo]
        public async Task<PageOutputDto<UserListOutputDto>> GetUserPage([FromQuery] PageInputDto dto)
        {
            return await _userService.PageAsync(dto);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("删除用户信息")]
        [ModuleInfo]
        public async Task<StatusResult> Delete(CommonIdInputDto dto)
        {
            return await _userService.DeleteAsync(dto.Id);
        }
    }
}
