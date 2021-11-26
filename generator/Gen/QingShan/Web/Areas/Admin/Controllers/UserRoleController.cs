using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.UserRole;
using QingShan.Services.UserRole.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 用户角色实体
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class UserRoleController
	{

        private readonly IUserRoleContract _iUserRoleContract;

        public UserRoleController(IUserRoleContract iUserRoleContract)
        {
            _iUserRoleContract = iUserRoleContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<UserRoleOutputDto>> PageAsync(PageUserRoleInputDto dto)
            => await _iUserRoleContract.PageAsync(dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(UserRoleInputDto input)
            => await _iUserRoleContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(UserRoleInputDto input)
            => await _iUserRoleContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync(string id)
            => await _iUserRoleContract.DeleteAsync(id);
	}
}
