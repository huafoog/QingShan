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
using QingShan;
using Estimate.Api.EstimationPredrilling;
using Estimate.Api.EstimationPredrilling.Dto;
using Estimate.Api.Data.Entities;
namespace Estimate.Api.EstimationPredrilling
{
    /// <summary>
	/// 
    /// </summary>
    public class EstimationPredrillingService:IEstimationPredrillingContract,IScopeDependency
    {
        private readonly IRepository<EstimationPredrillingEntity> _estimationPredrillingRepository;
        public EstimationPredrillingService(IRepository<EstimationPredrillingEntity> estimationPredrillingRepository)
        {
            _estimationPredrillingRepository = estimationPredrillingRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<EstimationPredrillingOutputDto>> PageAsync(PageEstimationPredrillingInputDto dto)
        {
            return await _estimationPredrillingRepository.Select.ToPageResultAsync<EstimationPredrillingEntity,EstimationPredrillingOutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(EstimationPredrillingInputDto input)
        {
            var entity = input.Adapt<EstimationPredrillingEntity>();
            entity.Id = Snowflake.GenId();
            var result = await _estimationPredrillingRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(EstimationPredrillingInputDto input)
        {
            var data = await _estimationPredrillingRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }
            
            
            
            
            
            data.@(item.PropName) = input.@(item.PropName);
            
            
            
             
            
            
            
            
            
             
            
            
            data.@(item.PropName) = input.@(item.PropName);
            
            
            
            data.@(item.PropName) = input.@(item.PropName);
            
            
            
            data.@(item.PropName) = input.@(item.PropName);
            
            
            
            data.@(item.PropName) = input.@(item.PropName);
            
            
            
            data.@(item.PropName) = input.@(item.PropName);
            
            
            
            data.@(item.PropName) = input.@(item.PropName);
            
            
            int res = await _estimationPredrillingRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _estimationPredrillingRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
