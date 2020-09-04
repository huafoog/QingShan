﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QS.Core.Data;
using QS.Core.Dependency;
using QS.Core.Encryption;
using QS.Core.Extensions;
using QS.DataLayer.Entities;
using QS.ServiceLayer.Account.Dto;
using QS.ServiceLayer.Account.Dto.OutputDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.Account
{
    public class AccountService: IAccountService, IScopeDependency
    {

        private readonly EFContext _context;

        private readonly IConfiguration _config;

        public AccountService(EFContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult<AuthLoginOutputDto>> LoginAsync(LoginInputDto dto)
        {

            var user = await _context.Users.GetTrackEntities(o => o.UserName == dto.Account).Select(o=>new { 
                o.Id,
                o.NickName,
                o.UserName,  
                o.Password,
                o.Status,
                o.IsSuper
            }).FirstOrDefaultAsync();
            if (user == null) return new StatusResult<AuthLoginOutputDto>("账号或密码错误");
            if (user.Status!= EAdministratorStatus.Normal) return new StatusResult<AuthLoginOutputDto>($"当前账号状态为：{user.Status.ToDescription()}");
            var password = MD5Encrypt.Encrypt32(dto.Password);
            if (user.Password != password) return new StatusResult<AuthLoginOutputDto>("账号或密码错误");

            var model = new AuthLoginOutputDto()
            {
                Id = user.Id,
                NickName = user.NickName,
                UserName = user.UserName
            };
            return new StatusResult<AuthLoginOutputDto>(model); 
        }
    }
}
