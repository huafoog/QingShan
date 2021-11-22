﻿//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成

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

using Demo.Services.Permission;
using Demo.Services.Permission.Dto;
using Demo.Data.Entities;
namespace Demo.Services.Permission
{
    /// <summary>
	/// 权限
    /// </summary>
    public class PermissionService:IPermissionContract,IScopeDependency
    {
        private readonly IRepository<PermissionEntity> _permissionRepository;
        public PermissionService(IRepository<PermissionEntity> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<PermissionOutputDto>> PageAsync(PagePermissionInputDto dto)
        {
            return await _permissionRepository.Select.ToPageResultAsync<PermissionEntity,PermissionOutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(PermissionInputDto input)
        {
            var entity = input.Adapt<PermissionEntity>();
            entity.Id = Snowflake.GenId();
            var result = await _permissionRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(PermissionInputDto input)
        {
            var data = await _permissionRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }

            data.Code = input.Code;
            
            data.Component = input.Component;
            
            data.Icon = input.Icon;
            
            data.Name = input.Name;
            
            data.ParentId = input.ParentId;
            
            data.Path = input.Path;
            
            data.PermissionCode = input.PermissionCode;
            
            data.PermissionType = input.PermissionType;
            
            data.Remark = input.Remark;
            
            data.Sort = input.Sort;
                        int res = await _permissionRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _permissionRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}