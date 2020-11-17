using Microsoft.AspNetCore.Mvc;
using QS.Core.Data;
using QS.Core.Permission;
using QS.Core.Permission.Authorization;
using QS.Core.Web.Filter.Transaction;
using QS.ServiceLayer.System.Role;
using QS.ServiceLayer.System.Role.Dto.InputDto;
using QS.ServiceLayer.System.Role.Dto.OutputDto;
using System.ComponentModel;
using System.Threading.Tasks;

namespace QS.Core.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Description("角色管理")]
    [ModuleInfo(URL = "/Admin/Role/Index", Module = Data.Enums.ModuleEnum.System, Sort = 0)]
    public class RoleController : AdminBaseController
    {
        private readonly IRoleService _roleService;
        public readonly IUserInfo _userInfo;
        public RoleController(IRoleService roleService, IUserInfo userInfo)
        {
            _roleService = roleService;
            _userInfo = userInfo;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("新增角色")]
        [ModuleInfo]
        [TransactionInterceptor]
        public async Task<StatusResult> Add(RoleInputDto input)
        {
            return await _roleService.AddAsync(input);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改角色")]
        [ModuleInfo]
        [TransactionInterceptor]
        public async Task<StatusResult> Update(RoleInputDto input)
        {
            return await _roleService.UpdateAsync(input);
        }

        /// <summary>
        /// 获取角色详情信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("获取角色详情信息")]
        [ModuleInfo]
        public async Task<StatusResult<RoleOutputDto>> GetInfo(CommonIdInputDto dto)
        {
            return await _roleService.GetAsync(dto.Id);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取角色信息")]
        [ModuleInfo]
        public async Task<PageOutputDto<RoleOutputDto>> GetPage([FromQuery] PageInputDto dto)
        {
            return await _roleService.PageAsync(dto);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("删除角色信息")]
        [ModuleInfo]
        public async Task<StatusResult> Delete(CommonIdInputDto dto)
        {
            return await _roleService.DeleteAsync(dto.Id);
        }
    }
}
