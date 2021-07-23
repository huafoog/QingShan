using QingShan.DependencyInjection;
using QingShan.DataLayer.Entities;
using System.Threading.Tasks;
using QingShan.Data;
using QingShan.Services.Permission.Dto;
using QingShan.Permission;
using QingShan.DatabaseAccessor;
using Mapster;
using System;
using System.Collections.Generic;
using QingShan.Utilities;
using QingShan.Core.FreeSql;
using FreeSql;
using System.Linq;
using QingShan.Utilities;

namespace QingShan.Services.Permission
{
    /// <summary>
    /// 菜单服务实现
    /// </summary>
    public class MenuService : IMenuContract, IScopeDependency
    {

        private readonly IUserInfo _user;
        private readonly IRepository<MenuEntity> _menuRepository;

        public MenuService( IUserInfo user, IRepository<MenuEntity> menuRepository)
        {
            _user = user;
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<PermissionListOutputDto>> GetPageAsync(PageInputDto dto)
        {
            var data = await _menuRepository
               .Select
               .ToPageResultAsync<MenuEntity, PermissionListOutputDto>(
                   dto,
                   o => dto.Search.IsNull() || o.Code.Contains(dto.Search) || o.Name.Contains(dto.Search)
               );
            return data;
        }

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        public async Task<StatusResult<List<PermissionListOutputDto>>> GetPageTreeAsync()
        {
            var result = new StatusResult<List<PermissionListOutputDto>>();
            var data = await _menuRepository
               .Select
               .ToListAsync<PermissionListOutputDto>();

            var tree = TreeHelper.GetTreeByParentId(data);

            result.Data = tree;
            return result;
        }


        #region 菜单操作
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> InsertMenu(MenuInputDto dto)
        {
            var model = dto.Adapt<MenuEntity>();
            model.Id = Snowflake.GenId();
            if (_menuRepository.Where(o=>o.Code == dto.Code).Any())
            {
                return new StatusResult("当前菜单code已存在");
            }
            var res = await _menuRepository.InsertOrUpdateAsync(model);
            return new StatusResult(res == null, "操作失败");  
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateMenu(UpdateMenuInputDto dto)
        {
            if (dto.Id.IsNull())
            {
                return new StatusResult("未获取到菜单信息");
            }
            var model = dto.Adapt<MenuEntity>();
            var res = await _menuRepository.InsertOrUpdateAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 添加菜单集合
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<StatusResult> InsertMenu(MenuInputDto[] models)
        {
            var model = models.Adapt<List<MenuEntity>>(); ;
            var res = await _menuRepository.InsertAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteMenu(IdsInputDto dto)
        {
            var res = await _menuRepository.DeleteAsync(dto.Ids);
            return new StatusResult(res == 0, "操作失败");
        }

        #endregion
    }
}
