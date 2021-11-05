//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using Demo.Services.Menu;
using Demo.Services.Menu.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Demo.Controllers
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
