//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using Demo.Services.User;
using Demo.Services.User.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QingShan.Data;

namespace Demo.Controllers
{
    /// <summary>
	/// 用户表
    /// </summary>
	[ApiController]
    [Route("[controller]/[action]")]
    public class UserController
	{

        private readonly IUserContract _iUserContract;

        public UserController(IUserContract iUserContract)
        {
            _iUserContract = iUserContract;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageOutputDto<UserOutputDto>> PageAsync(PageUserInputDto dto)
            => await _iUserContract.PageAsync(dto);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> AddAsync(UserInputDto input)
            => await _iUserContract.AddAsync(input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> UpdateAsync(UserInputDto input)
            => await _iUserContract.UpdateAsync(input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]public async Task<StatusResult> DeleteAsync(string id)
            => await _iUserContract.DeleteAsync(id);
	}
}
