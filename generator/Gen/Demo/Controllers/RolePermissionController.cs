//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using Demo.Services.RolePermission;
using Demo.Services.RolePermission.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Demo.Controllers
{
    /// <summary>
	/// 角色模块
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class RolePermissionController
	{

        private readonly IRolePermissionContract _iRolePermissionContract;

        public RolePermissionController(IRolePermissionContract iRolePermissionContract)
        {
            _iRolePermissionContract = iRolePermissionContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<RolePermissionOutputDto>> PageAsync(PageRolePermissionInputDto dto)
            => await _iRolePermissionContract.PageAsync(dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(RolePermissionInputDto input)
            => await _iRolePermissionContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(RolePermissionInputDto input)
            => await _iRolePermissionContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync(string id)
            => await _iRolePermissionContract.DeleteAsync(id);
	}
}
