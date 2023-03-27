using System;
using System.Collections.Generic;
using System.Text;
using Estimate.Api.EstimationPredrilling;
using Estimate.Api.EstimationPredrilling.Dto;
using Estimate.Api.Data.Entities;
using QingShan.Data;
using System.Threading.Tasks;

namespace Estimate.Api.EstimationPredrilling
{
	/// <summary>
	/// 
    /// </summary>
	public interface IEstimationPredrillingContract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<EstimationPredrillingOutputDto>> PageAsync(PageEstimationPredrillingInputDto dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(EstimationPredrillingInputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(EstimationPredrillingInputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
