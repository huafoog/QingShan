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
using Estimate.Api.Services.EstimationLeaseholdPrice;
using Estimate.Api.Services.EstimationLeaseholdPrice.Dto;
using Estimate.Api.Data.Entities;
namespace Estimate.Api.Services.EstimationLeaseholdPrice
{
    /// <summary>
	/// 征租地单价
    /// </summary>
    public class EstimationLeaseholdPriceService:IEstimationLeaseholdPriceContract,IScopeDependency
    {
        private readonly IRepository<EstimationLeaseholdPriceEntity> _estimationLeaseholdPriceRepository;
        public EstimationLeaseholdPriceService(IRepository<EstimationLeaseholdPriceEntity> estimationLeaseholdPriceRepository)
        {
            _estimationLeaseholdPriceRepository = estimationLeaseholdPriceRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<EstimationLeaseholdPriceOutputDto>> PageAsync(PageEstimationLeaseholdPriceInputDto dto)
        {
            return await _estimationLeaseholdPriceRepository.Select.ToPageResultAsync<EstimationLeaseholdPriceEntity,EstimationLeaseholdPriceOutputDto>(dto,null);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult<EstimationLeaseholdPriceOutputDto>> GetByIdAsync(string id) => new StatusResult<EstimationLeaseholdPriceOutputDto>(await  _estimationLeaseholdPriceRepository.Select.Where(o => o.Id == id).FirstAsync<EstimationLeaseholdPriceOutputDto>());



        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(EstimationLeaseholdPriceInputDto input)
        {
            var entity = input.Adapt<EstimationLeaseholdPriceEntity>();
            entity.Id = Snowflake.GenId();
            var result = await _estimationLeaseholdPriceRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(EstimationLeaseholdPriceInputDto input)
        {
            var data = await _estimationLeaseholdPriceRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }
            
            
            
            
            data.AreaId = input.AreaId;
            
            
            
            
            
            
            
            
            
            data.Number = input.Number;
            
            
            
            data.Unit = input.Unit;
            
            
            int res = await _estimationLeaseholdPriceRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _estimationLeaseholdPriceRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
