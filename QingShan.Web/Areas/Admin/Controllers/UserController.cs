using Microsoft.AspNetCore.Mvc;
using QingShan.Attributes;
using QingShan.Core.FreeSql.UnitOfWork.TransactionInterceptor;
using QingShan.Data;
using QingShan.Permission;
using QingShan.Services.System.Role;
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
    public class UserController : AdminBaseController
    {
        private readonly IUserContract _userService;
        private readonly IRoleService _roleService;
        public readonly IUserInfo _userInfo;
        public UserController(IUserContract userService, IRoleService roleService, IUserInfo userInfo)
        {
            _userService = userService;
            _roleService = roleService;
            _userInfo = userInfo;
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

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改用户")]
        public async Task<StatusResult> Update(UserUpdateInputDto input)
        {
            return await _userService.UpdateAsync(input);
        }

        /// <summary>
        /// 修改自身信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改自身信息")]
        public async Task<StatusResult> SaveInfoAsync(SaveInfoInputDto input) => await _userService.SaveInfoAsync(input);

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取当前登录用户信息")]
        [LoggedIn]
        public async Task<StatusResult<UserPermissionOutputDto>> GetUserInfo()
        {
            return await _userService.GetUserInfoAsync(_userInfo.Id);
        }

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取用户详情信息")]
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
        public async Task<PageOutputDto<UserListOutputDto>> GetUserPage([FromQuery] SearchUserInputDto dto)
        {
            return await _userService.PageAsync(dto);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取角色列表")]
        public async Task<PageOutputDto<QingShan.Services.System.Role.Dto.OutputDto.RoleOutputDto>> GetUserRoleData()
        {
            var result =  await _roleService.PageAsync(new Services.System.Role.Dto.InputDto.PageRoleInputDto() { Enabled = true, PageNo = 1, PageSize = 999 });
            return result;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("删除用户信息")]

        public async Task<StatusResult> Delete(CommonIdInputDto dto)
        {
            return await _userService.DeleteAsync(dto.Id);
        }

        /// <summary>
        /// 禁用用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("禁用用户信息")]
        public async Task<StatusResult> DisableAsync(CommonIdInputDto dto)
        {
            return await _userService.DisableAsync(dto.Id);
        }
    }
}
