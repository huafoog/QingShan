using QS.Core.Data;
using QS.DataLayer.Entities;
using QS.ServiceLayer.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.Permission
{
    public interface IModuleService
    {
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<StatusResult> InsertModules(ModuleInputDto dto);

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateModules(ModuleInputDto dto);

        /// <summary>
        /// 添加模块集合
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<StatusResult> InsertModules(ModuleEntity[] models);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         Task<StatusResult> DeleteModule(Guid id);

         Task CreateModules(List<ModuleInfo> moduleInfos);
    }
}
