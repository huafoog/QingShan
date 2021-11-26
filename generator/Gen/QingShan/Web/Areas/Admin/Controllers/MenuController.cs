using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.Menu;
using QingShan.Services.Menu.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 菜单
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class MenuController
	{

        private readonly IMenuContract _iMenuContract;

        public MenuController(IMenuContract iMenuContract)
        {
            _iMenuContract = iMenuContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<MenuOutputDto>> PageAsync(PageMenuInputDto dto)
            => await _iMenuContract.PageAsync(dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(MenuInputDto input)
            => await _iMenuContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(MenuInputDto input)
            => await _iMenuContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync(string id)
            => await _iMenuContract.DeleteAsync(id);
	}
}
