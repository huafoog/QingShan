using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using QingShan.Data;
using QingShan.Services.Permission.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QingShan.Services.Permission
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public interface IMenuContract
    {

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<MenuListOutputDto>> GetPageAsync(PageInputDto dto);

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult> InsertMenu(MenuInputDto dto);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateMenu(UpdateMenuInputDto dto);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteMenu(IdsInputDto dto);
    }
}
