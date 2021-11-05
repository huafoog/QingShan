//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using Demo.Services.Permission;
using Demo.Services.Permission.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Demo.Controllers
{
    /// <summary>
	/// 权限
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class PermissionController
	{

        private readonly IPermissionContract _iPermissionContract;

        public PermissionController(IPermissionContract iPermissionContract)
        {
            _iPermissionContract = iPermissionContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<PermissionOutputDto>> PageAsync(PagePermissionInputDto dto)
            => await _iPermissionContract.PageAsync(dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(PermissionInputDto input)
            => await _iPermissionContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(PermissionInputDto input)
            => await _iPermissionContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync(string id)
            => await _iPermissionContract.DeleteAsync(id);
	}
}
