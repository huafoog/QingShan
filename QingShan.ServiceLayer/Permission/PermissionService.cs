using Mapster;
using QingShan.Data;
using QingShan.DatabaseAccessor;
using QingShan.DependencyInjection;
using QingShan.DataLayer.Entities;
using QingShan.Services.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QingShan.Core.FreeSql;
using QingShan.Utilities;
using Panda.DynamicWebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using QingShan.Services.User;
using QingShan.Common.Data;
using QingShan.Cache;
using QingShan.Permission;
using QingShan.Services.Permission.Dto.OutputDto;

namespace QingShan.Services.Permission
{
    /// <summary>
    /// 权限服务
    /// </summary>
    [DynamicWebApi]
    [ApiDescriptionSettings("Admin")]
    public class PermissionService : IPermissionContract, IScopeDependency
    {
        private readonly IRepository<DataLayer.Entities.PermissionEntity> _modelRepository;
        private readonly IRepository<RolePermissionEntity> _rolePermissionRepository;
        private readonly IRepository<RoleEntity> _roleRepository;
        private readonly ICache _cache;
        private readonly IUserInfo _user;

        public PermissionService(
            IRepository<DataLayer.Entities.PermissionEntity> modelRepository,
            IRepository<RolePermissionEntity> rolePermissionRepository,
            IRepository<RoleEntity> roleRepository,
            ICache cache,
             IUserInfo user
            )
        {
            _modelRepository = modelRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
            _cache = cache;
            _user = user;
        }

        #region 权限操作

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        public async Task<StatusResult<List<PermissionListOutputDto>>> GetPageTreeAsync()
        {
            var result = new StatusResult<List<PermissionListOutputDto>>();
            var data = await _modelRepository
               .Select
               .ToListAsync<PermissionListOutputDto>();

            var tree = TreeHelper.GetTreeByParentId(data);

            result.Data = tree;
            return result;
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> InsertPermission(PermissionInputDto dto)
        {
            if (dto.PermissionType == DataLayer.Enums.PermissionType.Button)
            {
                if (dto.PermissionCode.IsNull())
                {
                    return new StatusResult("请输入权限编码");
                }
            }
            var model = dto.Adapt<PermissionEntity>();
            model.Id = Snowflake.GenId();
            var res = await _modelRepository.InsertOrUpdateAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdatePermission(UpdatePermissionInputDto dto)
        {
            if (dto.Id.IsNull())
            {
                return new StatusResult("未获取到权限信息");
            }
            if (dto.PermissionType == DataLayer.Enums.PermissionType.Button)
            {
                if (dto.PermissionCode.IsNull())
                {
                    return new StatusResult("请输入权限编码");
                }
            }
            var model = dto.Adapt<DataLayer.Entities.PermissionEntity>();
            var res = await _modelRepository.InsertOrUpdateAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 添加权限集合
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<StatusResult> InsertPermission(PermissionInputDto[] models)
        {
            var model = models.Adapt<List<DataLayer.Entities.PermissionEntity>>(); ;
            var res = await _modelRepository.InsertAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> Delete(IdsInputDto dto)
        {

            var checkRoot = await _modelRepository.Where(o => dto.Ids.Contains(o.ParentId)).AnyAsync();

            if (checkRoot)
            {
                return new StatusResult("根目录下存在数据无法删除");
            }
            var res = await _modelRepository.DeleteAsync(dto.Ids);
            return new StatusResult(res == 0, "操作失败");
        }

        #endregion
        #region 用户权限
        /// <summary>
        /// 获取角色权限树形结构
        /// </summary>
        /// <returns></returns>
        public async Task<StatusResult<RoleTreeOutputDto>> GetRoleTreeAsync(RoleIdInputDto dto)
        {
            var result = new StatusResult<RoleTreeOutputDto>();
            //当前权限
            var rolePermission = await _rolePermissionRepository.Select.From<RoleEntity, PermissionEntity>((s, b, c) =>
             s.InnerJoin(a => a.RoleId == b.Id)
             .InnerJoin(a => a.PermissionId == c.Id)
            ).Where((a, b, c) => a.RoleId == dto.RoleId).ToListAsync((a, b, c) => a.PermissionId);

            var data = await _modelRepository
               .Select
               .ToListAsync<PermissionListOutputDto>();

            var tree = TreeHelper.GetTreeByParentId(data);

            result.Data = new RoleTreeOutputDto()
            {
                Data = tree,
                DefaultSelectedKeys = rolePermission,
                List = data
            };
            return result;
        }

        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> SetRolePermissionAsync(SetRolePermissionInputDto dto)
        {
            var result = new StatusResult();
            var role = await _roleRepository.Where(o => o.Id == dto.RoleId && o.Enabled).AnyAsync();
            if (!role)
            {
                result.SetErrorMessage("未获取到角色信息");
                return result;
            }
            await _rolePermissionRepository.DeleteAsync(o => o.RoleId == dto.RoleId);
            var data = new List<RolePermissionEntity>();
            foreach (var item in dto.PermissionIds)
            {
                var model = new RolePermissionEntity()
                {
                    Id = Snowflake.GenId(),
                    PermissionId = item,
                    RoleId = dto.RoleId
                };
                data.Add(model);
            }
            await _rolePermissionRepository.InsertAsync(data);
            return result;
        }

        #endregion

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckPermission(string code)
        {
            var permission = await GetPermissionsAsync();
            return permission.Contains(code);
        }


        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetPermissionsAsync()
        {
            var key = CacheKey.UserPermissions.ToFormat(_user.Id);
            var userPermissoins = await _cache.GetAsync<List<string>>(key);
            if (userPermissoins == null)
            {
                //当前有权限的操作
                userPermissoins = await _rolePermissionRepository.Orm.Select<UserEntity, UserRoleEntity, RoleEntity, RolePermissionEntity, PermissionEntity>()
                    .InnerJoin((a, b, c, d, e) => a.Id == b.UserId)
                    .InnerJoin((a, b, c, d, e) => b.RoleId == c.Id)
                    .InnerJoin((a, b, c, d, e) => c.Id == d.RoleId)
                    .InnerJoin((a, b, c, d, e) => d.PermissionId == e.Id)
                    .Where((a, b, c, d, e) => a.Id == _user.Id && e.PermissionType == DataLayer.Enums.PermissionType.Button)
                    .ToListAsync((a, b, c, d, e) => e.PermissionCode);
                await _cache.SetAsync(key, userPermissoins, 60 * 60 * 24);
            }
            return userPermissoins;
        }

        /// <summary>
        /// 获取用户菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetUserMeunAsync()
        {
            //当前权限
            var data = await _rolePermissionRepository.Orm.Select<UserEntity, UserRoleEntity, RoleEntity, RolePermissionEntity, PermissionEntity>()
                    .InnerJoin((a, b, c, d, e) => a.Id == b.UserId)
                    .InnerJoin((a, b, c, d, e) => b.RoleId == c.Id)
                    .InnerJoin((a, b, c, d, e) => c.Id == d.RoleId)
                    .InnerJoin((a, b, c, d, e) => d.PermissionId == e.Id)
                    .Where((a, b, c, d, e) => a.Id == _user.Id && e.PermissionType != DataLayer.Enums.PermissionType.Button)
                    .ToListAsync((a, b, c, d, e) => e.Path);
            return data;
        }
    }
}
