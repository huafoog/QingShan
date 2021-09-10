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

        public PermissionService(
            IRepository<DataLayer.Entities.PermissionEntity> modelRepository,
            IRepository<RolePermissionEntity> rolePermissionRepository,
            IRepository<RoleEntity> roleRepository
            )
        {
            _modelRepository = modelRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
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
            var res = await _modelRepository.DeleteAsync(dto.Ids);
            return new StatusResult(res > 0, "操作失败");
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
            await _rolePermissionRepository.DeleteAsync(o=>o.RoleId == dto.RoleId);
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
        public async Task<bool> CheckPermission()
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
