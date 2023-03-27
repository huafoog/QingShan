using System;
using System.Collections.Generic;
using System.Text;
using $model.ContractNamespace;
using $model.DtoNamespace;
using $model.EntityNamespace;
using QingShan.Data;
using System.Threading.Tasks;

namespace $model.Namespace
{
	/// <summary>
	/// $model.Remark
    /// </summary>
	public interface I${model.Name}Contract
	{
		/// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageOutputDto<${model.Name}OutputDto>> PageAsync(Page${model.Name}InputDto dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(${model.Name}InputDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> UpdateAsync(${model.Name}InputDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult> DeleteAsync(string id);
	}
}
