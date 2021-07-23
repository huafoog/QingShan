using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using QingShan.Data;
using QingShan.DataLayer.Entities;
using QingShan.Services.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QingShan.Services.Permission
{
    /// <summary>
    /// 权限
    /// </summary>
    public interface IPermissionContract
    {

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        Task<StatusResult<List<PermissionListOutputDto>>> GetPageTreeAsync();

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult> InsertPermission(PermissionInputDto dto);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult> UpdatePermission(UpdatePermissionInputDto dto);

        /// <summary>
        /// 添加权限集合
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<StatusResult> InsertPermission(PermissionInputDto[] models);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult> Delete(IdsInputDto dto);

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckPermission();
    }   
}
