using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.RolePermission;
using QingShan.Services.RolePermission.Dto;
using QingShan.DataLayer.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace QingShan.Services.RolePermission
{
	/// <summary>
	/// 角色模块
    /// </summary>
	public interface IRolePermissionContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<RolePermissionOutputDto>> PageAsync(PageRolePermissionInputDto dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(RolePermissionInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(RolePermissionInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
