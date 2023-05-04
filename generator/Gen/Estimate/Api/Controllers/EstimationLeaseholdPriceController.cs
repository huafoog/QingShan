using System;
using System.Collections.Generic;
using System.Text;
using Estimate.Api.Services.EstimationLeaseholdPrice;
using Estimate.Api.Services.EstimationLeaseholdPrice.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Estimate.Api.Controllers
{
    /// <summary>
	/// 征租地单价
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class EstimationLeaseholdPriceController
	{

        private readonly IEstimationLeaseholdPriceContract _iEstimationLeaseholdPriceContract;

        public EstimationLeaseholdPriceController(IEstimationLeaseholdPriceContract iEstimationLeaseholdPriceContract)
        {
            _iEstimationLeaseholdPriceContract = iEstimationLeaseholdPriceContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<EstimationLeaseholdPriceOutputDto>> PageAsync([FromQuery]PageEstimationLeaseholdPriceInputDto dto)
            => await _iEstimationLeaseholdPriceContract.PageAsync(dto);
        
        /// <summary>
        /// 获取详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<StatusResult<EstimationLeaseholdPriceOutputDto>> GetByIdAsync(string id)
            => await _iEstimationLeaseholdPriceContract.GetByIdAsync(id);


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(EstimationLeaseholdPriceInputDto input)
            => await _iEstimationLeaseholdPriceContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(EstimationLeaseholdPriceInputDto input)
            => await _iEstimationLeaseholdPriceContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync([FromBody]CommonIdInputDto dto)
            => await _iEstimationLeaseholdPriceContract.DeleteAsync(dto.Id);
        }
    }
