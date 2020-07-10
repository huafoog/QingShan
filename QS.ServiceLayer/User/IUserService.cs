﻿using QS.Core.Data;
using QS.ServiceLayer.User.Dtos.InputDto;
using QS.ServiceLayer.User.Dtos.OutputDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.User
{
    public interface IUserService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StatusResult<UserGetOutputDto>> GetAsync(int id);

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<StatusResult> AddAsync(UserAddInputDto input);
    }
}
