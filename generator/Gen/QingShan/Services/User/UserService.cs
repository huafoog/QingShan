using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QingShan.DependencyInjection;
using QingShan.Core.FreeSql;
using QingShan.Data;
using FreeSql;
using Mapster;
using QingShan.Utilities;

using QingShan.Services.User;
using QingShan.Services.User.Dto;
using QingShan.DataLayer.Entities;
namespace QingShan.Services.User
{
    /// <summary>
	/// 用户表
    /// </summary>
    public class UserService:IUserContract,IScopeDependency
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<UserOutputDto>> PageAsync(PageUserInputDto dto)
        {
            return await _userRepository.Select.ToPageResultAsync<User,UserOutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(UserInputDto input)
        {
            var entity = input.Adapt<User>();
            entity.Id = Snowflake.GenId();
            var result = await _userRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(UserInputDto input)
        {
            var data = await _userRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }

            data.Avatar = input.Avatar;
            
            data.DepartmentId = input.DepartmentId;
            
            data.IsSuper = input.IsSuper;
            
            data.LastLoginIp = input.LastLoginIp;
            
            data.LastLoginTime = input.LastLoginTime;
            
            data.NickName = input.NickName;
            
            data.Password = input.Password;
            
            data.Phone = input.Phone;
            
            data.RealName = input.RealName;
            
            data.Remark = input.Remark;
            
            data.Status = input.Status;
            
            data.UpdateDateTime = input.UpdateDateTime;
            
            data.UserName = input.UserName;
                        int res = await _userRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _userRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
