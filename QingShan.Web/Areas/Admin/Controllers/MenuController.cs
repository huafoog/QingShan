using Microsoft.AspNetCore.Mvc;
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
    /// 菜单
    /// </summary>
    public class MenuController: AdminBaseController
    {
        private readonly IMenuContract _menuContract;

        public MenuController(IMenuContract menuContract)
        {
            _menuContract = menuContract;
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<StatusResult> InsertMenu(MenuInputDto dto) => await _menuContract.InsertMenu(dto);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<PermissionListOutputDto>> GetPage([FromQuery]PageInputDto dto) => await _menuContract.GetPageAsync(dto);

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<StatusResult<List<PermissionListOutputDto>>> GetPageTree() => await _menuContract.GetPageTreeAsync();

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<StatusResult> UpdateMenu(UpdateMenuInputDto dto) => await _menuContract.UpdateMenu(dto);
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<StatusResult> DeleteMenu(IdsInputDto dto) => await _menuContract.DeleteMenu(dto);

    }
}
