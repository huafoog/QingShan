using System;
using System.Collections.Generic;
using System.Text;
using Estimate.Api.Services.EstimationLeaseholdPrice;
using Estimate.Api.Services.EstimationLeaseholdPrice.Dto;
using Estimate.Api.Data.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace Estimate.Api.Services.EstimationLeaseholdPrice
{
	/// <summary>
	/// 征租地单价
    /// </summary>
	public interface IEstimationLeaseholdPriceContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<EstimationLeaseholdPriceOutputDto>> PageAsync(PageEstimationLeaseholdPriceInputDto dto);

         /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult<EstimationLeaseholdPriceOutputDto>> GetByIdAsync(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(EstimationLeaseholdPriceInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(EstimationLeaseholdPriceInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
