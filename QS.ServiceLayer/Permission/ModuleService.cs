using Mapster;
using QS.Core.Data;
using QS.Core.DatabaseAccessor;
using QS.Core.Dependency;
using QS.Core.Extensions;
using QS.DataLayer.Entities;
using QS.ServiceLayer.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.ServiceLayer.Permission
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public class ModuleService : IModuleService, IScopeDependency
    {
        private readonly IRepository<ModuleEntity, Guid> _modelRepository;
        public ModuleService(
            IRepository<ModuleEntity, Guid> modelRepository
            )
        {
            _modelRepository = modelRepository;
        }

        #region 模块操作
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> InsertModules(ModuleInputDto dto)
        {
            var model = dto.Adapt<ModuleEntity>(); ;
            var res = await _modelRepository.InsertOrUpdateAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateModules(ModuleInputDto dto)
        {
            if (!dto.Id.HasValue)
            {
                return new StatusResult("未获取到模块信息");
            }
            var model = dto.Adapt<ModuleEntity>(); ;
            var res = await _modelRepository.InsertOrUpdateAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 添加模块集合
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<StatusResult> InsertModules(ModuleEntity[] models)
        {
            var model = models.Adapt<List<ModuleEntity>>(); ;
            var res = await _modelRepository.InsertAsync(model);
            return new StatusResult(res == null, "操作失败");
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteModule(Guid id)
        {
            var res = await _modelRepository.DeleteAsync(id);
            return new StatusResult(res > 0, "操作失败");
        }

        #endregion

        /// <summary>
        /// 创建模块信息 
        /// </summary>
        public async Task CreateModules(List<ModuleInfo> moduleInfos)
        {
            //删除数据库中多余的模块
            List<ModuleEntity> modules = _modelRepository.Select.ToList();
            var positionModules = modules.Select(m => new { m.Id, m.Code })
                .OrderByDescending(m => m.Id).ToArray();
            string[] deletePositions = positionModules.Select(m => m.Code)
                .Except(moduleInfos.Select(n => n.Code))
                .ToArray();
            Guid[] deleteModuleIds = positionModules.Where(m => deletePositions.Contains(m.Code)).Select(m => m.Id).ToArray();

            await _modelRepository.DeleteAsync(o => deleteModuleIds.Any(a => a == o.Id));

            var models = modules.Adapt<List<ModuleEntity>>();
            //新增或更新传入的模块
            foreach (var info in moduleInfos)
            {
                ModuleEntity module = modules.FirstOrDefault(m => m.Code == info.Code);
                //新增
                if (module == null)
                {
                    //无上级
                    if (info.Pid != Guid.Empty)
                    {
                        var pModule = modules.FirstOrDefault(m => m.Code == info.Code);
                        if (pModule != null)
                        {
                            //当前父级存在数据库
                            info.Id = pModule.Id;
                        }
                    }
                    await _modelRepository.InsertAsync(info.Adapt<ModuleEntity>());
                }
                //else //更新
                //{

                //}
            }
            //string[] havaModuleCD = positionModules.Select(m => m.FNC_CD)
            //    .ToArray();
            ////已存在的模块
            //var havaModules = moduleInfos.Select(o => havaModuleCD.Any(p => p == o.FNC_CD)).ToList();
            //foreach (var item in havaModules)
            //{

            //}
        }

    }
}
