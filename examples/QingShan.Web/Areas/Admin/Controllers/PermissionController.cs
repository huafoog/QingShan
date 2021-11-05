using Microsoft.AspNetCore.Mvc;
using QingShan.Core.FreeSql.UnitOfWork.Attributes;
using QingShan.Core.FreeSql.UnitOfWork.TransactionInterceptor;
using QingShan.Data;
using QingShan.Services.Permission;
using QingShan.Services.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    public partial class PermissionController : AdminBaseController
    {
        private readonly IPermissionContract _permissionContract;

        public PermissionController(IPermissionContract permissionContract)
        {
            _permissionContract = permissionContract;
        }


        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        [HttpGet] public async Task<StatusResult<List<PermissionListOutputDto>>> GetPageTree() => await _permissionContract.GetPageTreeAsync();

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        [HttpGet] public async Task<StatusResult<RoleTreeOutputDto>> GetRoleTreeAsync([FromQuery] RoleIdInputDto dto) => await _permissionContract.GetRoleTreeAsync(dto);


        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Transaction]
        public Task<StatusResult> SetRolePermissionAsync(SetRolePermissionInputDto dto) => _permissionContract.SetRolePermissionAsync(dto);

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<StatusResult> InsertPermission(PermissionInputDto dto) => _permissionContract.InsertPermission(dto);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<StatusResult> UpdatePermission(UpdatePermissionInputDto dto) => _permissionContract.UpdatePermission(dto);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<StatusResult> Delete(IdsInputDto dto) => _permissionContract.Delete(dto);
    }
}
