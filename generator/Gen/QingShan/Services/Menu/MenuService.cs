using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QingShan.DependencyInjection;
using QingShan.Core.FreeSql;
using QingShan.Data;
using FreeSql;
using Mapster;
using QingShan.Utilities;

using QingShan.Services.Menu;
using QingShan.Services.Menu.Dto;
using QingShan.DataLayer.Entities;
namespace QingShan.Services.Menu
{
    /// <summary>
	/// 菜单
    /// </summary>
    public class MenuService:IMenuContract,IScopeDependency
    {
        private readonly IRepository<Menu> _menuRepository;
        public MenuService(IRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<MenuOutputDto>> PageAsync(PageMenuInputDto dto)
        {
            return await _menuRepository.Select.ToPageResultAsync<Menu,MenuOutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(MenuInputDto input)
        {
            var entity = input.Adapt<Menu>();
            entity.Id = Snowflake.GenId();
            var result = await _menuRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(MenuInputDto input)
        {
            var data = await _menuRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }

            data.Code = input.Code;
            
            data.Component = input.Component;
            
            data.Icon = input.Icon;
            
            data.Name = input.Name;
            
            data.ParentId = input.ParentId;
            
            data.Redirect = input.Redirect;
                        int res = await _menuRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _menuRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
