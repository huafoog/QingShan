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
using Estimate.Api.Services.EstimationLeaseholdWorkload;
using Estimate.Api.Services.EstimationLeaseholdWorkload.Dto;
using Estimate.Api.Data.Entities;
namespace Estimate.Api.Services.EstimationLeaseholdWorkload
{
    /// <summary>
	/// 征租地工作量
    /// </summary>
    public class EstimationLeaseholdWorkloadService:IEstimationLeaseholdWorkloadContract,IScopeDependency
    {
        private readonly IRepository<EstimationLeaseholdWorkloadEntity> _estimationLeaseholdWorkloadRepository;
        public EstimationLeaseholdWorkloadService(IRepository<EstimationLeaseholdWorkloadEntity> estimationLeaseholdWorkloadRepository)
        {
            _estimationLeaseholdWorkloadRepository = estimationLeaseholdWorkloadRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<EstimationLeaseholdWorkloadOutputDto>> PageAsync(PageEstimationLeaseholdWorkloadInputDto dto)
        {
            return await _estimationLeaseholdWorkloadRepository.Select.ToPageResultAsync<EstimationLeaseholdWorkloadEntity,EstimationLeaseholdWorkloadOutputDto>(dto,null);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult<EstimationLeaseholdWorkloadOutputDto>> GetByIdAsync(string id) => new StatusResult<EstimationLeaseholdWorkloadOutputDto>(await  _estimationLeaseholdWorkloadRepository.Select.Where(o => o.Id == id).FirstAsync<EstimationLeaseholdWorkloadOutputDto>());



        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(EstimationLeaseholdWorkloadInputDto input)
        {
            var entity = input.Adapt<EstimationLeaseholdWorkloadEntity>();
            entity.Id = Snowflake.GenId();
            var result = await _estimationLeaseholdWorkloadRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(EstimationLeaseholdWorkloadInputDto input)
        {
            var data = await _estimationLeaseholdWorkloadRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }
            
            
            
            
            data.AreaId = input.AreaId;
            
            
            
            
            
            
            
            
            
            data.Fee = input.Fee;
            
            
            
            data.Unit = input.Unit;
            
            
            int res = await _estimationLeaseholdWorkloadRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _estimationLeaseholdWorkloadRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
