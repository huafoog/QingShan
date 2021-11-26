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

using QingShan.Services.RolePermission;
using QingShan.Services.RolePermission.Dto;
using QingShan.DataLayer.Entities;
namespace QingShan.Services.RolePermission
{
    /// <summary>
	/// 角色模块
    /// </summary>
    public class RolePermissionService:IRolePermissionContract,IScopeDependency
    {
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        public RolePermissionService(IRepository<RolePermission> rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<RolePermissionOutputDto>> PageAsync(PageRolePermissionInputDto dto)
        {
            return await _rolePermissionRepository.Select.ToPageResultAsync<RolePermission,RolePermissionOutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(RolePermissionInputDto input)
        {
            var entity = input.Adapt<RolePermission>();
            entity.Id = Snowflake.GenId();
            var result = await _rolePermissionRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(RolePermissionInputDto input)
        {
            var data = await _rolePermissionRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }

            data.PermissionId = input.PermissionId;
            
            data.RoleId = input.RoleId;
                        int res = await _rolePermissionRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _rolePermissionRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
