using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.Role;
using QingShan.Services.Role.Dto;
using QingShan.DataLayer.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace QingShan.Services.Role
{
	/// <summary>
	/// 角色模型
    /// </summary>
	public interface IRoleContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<RoleOutputDto>> PageAsync(PageRoleInputDto dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(RoleInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(RoleInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
