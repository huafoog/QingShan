using System;
using System.Collections.Generic;
using System.Text;
using Estimate.Api.Services.EstimationLeaseholdWorkload;
using Estimate.Api.Services.EstimationLeaseholdWorkload.Dto;
using Estimate.Api.Data.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace Estimate.Api.Services.EstimationLeaseholdWorkload
{
	/// <summary>
	/// 征租地工作量
    /// </summary>
	public interface IEstimationLeaseholdWorkloadContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<EstimationLeaseholdWorkloadOutputDto>> PageAsync(PageEstimationLeaseholdWorkloadInputDto dto);

         /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult<EstimationLeaseholdWorkloadOutputDto>> GetByIdAsync(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(EstimationLeaseholdWorkloadInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(EstimationLeaseholdWorkloadInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
