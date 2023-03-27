using System;
using System.Collections.Generic;
using System.Text;
using Estimate.Api.EstimationPredrilling;
using @Model.DtoNamespace;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Estimate.Api.Controllers
{
    /// <summary>
	/// 
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class EstimationPredrillingController
	{

        private readonly IEstimationPredrillingContract _iEstimationPredrillingContract;

        public EstimationPredrillingController(IEstimationPredrillingContract iEstimationPredrillingContract)
        {
            _iEstimationPredrillingContract = iEstimationPredrillingContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<EstimationPredrillingOutputDto>> PageAsync(PageEstimationPredrillingInputDto dto)
            => await _iEstimationPredrillingContract.PageAsync(dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(EstimationPredrillingInputDto input)
            => await _iEstimationPredrillingContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(EstimationPredrillingInputDto input)
            => await _iEstimationPredrillingContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync(string id)
            => await _iEstimationPredrillingContract.DeleteAsync(id);
	}
}
