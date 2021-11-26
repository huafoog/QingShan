using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.Menu;
using QingShan.Services.Menu.Dto;
using QingShan.DataLayer.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace QingShan.Services.Menu
{
	/// <summary>
	/// 菜单
    /// </summary>
	public interface IMenuContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<MenuOutputDto>> PageAsync(PageMenuInputDto dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(MenuInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(MenuInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
