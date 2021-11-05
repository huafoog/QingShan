//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using Demo.Services.Permission;
using Demo.Services.Permission.Dto;
using Demo.Data.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace Demo.Services.Permission
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
