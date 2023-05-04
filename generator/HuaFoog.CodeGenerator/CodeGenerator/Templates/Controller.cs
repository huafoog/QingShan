using System;
using System.Collections.Generic;
using System.Text;
using $model.ContractNamespace;
using $model.DtoNamespace;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace $model.Namespace
{
    /// <summary>
	/// $model.Remark
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class ${model.Name}Controller
	{

        private readonly I${model.Name}Contract _i${model.Name}Contract;

        public ${model.Name}Controller(I${model.Name}Contract i${model.Name}Contract)
        {
            _i${model.Name}Contract = i${model.Name}Contract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<${model.Name}OutputDto>> PageAsync([FromQuery]Page${model.Name}InputDto dto)
            => await _i${model.Name}Contract.PageAsync(dto);
        
        /// <summary>
        /// 获取详情数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<StatusResult<${model.Name}OutputDto>> GetByIdAsync(string id)
            => await _i${model.Name}Contract.GetByIdAsync(id);


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(${model.Name}InputDto input)
            => await _i${model.Name}Contract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(${model.Name}InputDto input)
            => await _i${model.Name}Contract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync([FromBody]CommonIdInputDto dto)
            => await _i${model.Name}Contract.DeleteAsync(dto.Id);
        }
    }
