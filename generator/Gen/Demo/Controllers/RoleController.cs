//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using Demo.Services.Role;
using Demo.Services.Role.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Demo.Controllers
{
    /// <summary>
	/// 角色模型
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class RoleController
	{

        private readonly IRoleContract _iRoleContract;

        public RoleController(IRoleContract iRoleContract)
        {
            _iRoleContract = iRoleContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<RoleOutputDto>> PageAsync(PageRoleInputDto dto)
            => await _iRoleContract.PageAsync(dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(RoleInputDto input)
            => await _iRoleContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(RoleInputDto input)
            => await _iRoleContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync(string id)
            => await _iRoleContract.DeleteAsync(id);
	}
}
