using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.User;
using QingShan.Services.User.Dto;
using QingShan.DataLayer.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace QingShan.Services.User
{
	/// <summary>
	/// 用户表
    /// </summary>
	public interface IUserContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<UserOutputDto>> PageAsync(PageUserInputDto dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(UserInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(UserInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
