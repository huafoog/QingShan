using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.Permission;
using QingShan.Services.Permission.Dto;
using QingShan.DataLayer.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace QingShan.Services.Permission
{
	/// <summary>
	/// 权限
    /// </summary>
	public interface IPermissionContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<PermissionOutputDto>> PageAsync(PagePermissionInputDto dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(PermissionInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(PermissionInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
