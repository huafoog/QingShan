using System;
using System.Collections.Generic;
using System.Text;
using Estimate.Api.Services.EstimationLeaseholdWorkload;
using Estimate.Api.Services.EstimationLeaseholdWorkload.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Estimate.Api.Controllers
{
    /// <summary>
	/// 征租地工作量
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class EstimationLeaseholdWorkloadController
	{

        private readonly IEstimationLeaseholdWorkloadContract _iEstimationLeaseholdWorkloadContract;

        public EstimationLeaseholdWorkloadController(IEstimationLeaseholdWorkloadContract iEstimationLeaseholdWorkloadContract)
        {
            _iEstimationLeaseholdWorkloadContract = iEstimationLeaseholdWorkloadContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<EstimationLeaseholdWorkloadOutputDto>> PageAsync([FromQuery]PageEstimationLeaseholdWorkloadInputDto dto)
            => await _iEstimationLeaseholdWorkloadContract.PageAsync(dto);
        
        /// <summary>
        /// 获取详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<StatusResult<EstimationLeaseholdWorkloadOutputDto>> GetByIdAsync(string id)
            => await _iEstimationLeaseholdWorkloadContract.GetByIdAsync(id);


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(EstimationLeaseholdWorkloadInputDto input)
            => await _iEstimationLeaseholdWorkloadContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(EstimationLeaseholdWorkloadInputDto input)
            => await _iEstimationLeaseholdWorkloadContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync([FromBody]CommonIdInputDto dto)
            => await _iEstimationLeaseholdWorkloadContract.DeleteAsync(dto.Id);
        }
    }
