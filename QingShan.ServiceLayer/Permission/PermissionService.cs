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

namespace QingShan.Services.Permission
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService : IPermissionContract, IScopeDependency
    {
        private readonly IRepository<DataLayer.Entities.PermissionEntity> _modelRepository;
        public PermissionService(
            IRepository<DataLayer.Entities.PermissionEntity> modelRepository
            )
        {
            _modelRepository = modelRepository;
        }

        #region 权限操作
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
            var model = dto.Adapt<DataLayer.Entities.PermissionEntity>(); ;
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteModule(string id)
        {
            var res = await _modelRepository.DeleteAsync(id);
            return new StatusResult(res > 0, "操作失败");
        }

        #endregion


        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckPermission()
        {
            return true;
        }
    }
}
