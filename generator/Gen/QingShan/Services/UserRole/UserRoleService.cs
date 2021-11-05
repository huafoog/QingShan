//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成

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

using QingShan.Services.UserRole;
using QingShan.Services.UserRole.Dto;
using QingShan.DataLayer.Entities;
namespace QingShan.Services.UserRole
{
    /// <summary>
	/// 用户角色实体
    /// </summary>
    public class UserRoleService:IUserRoleContract,IScopeDependency
    {
        private readonly IRepository<UserRoleEntity> _userRoleRepository;
        public UserRoleService(IRepository<UserRoleEntity> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<UserRoleOutputDto>> PageAsync(PageUserRoleInputDto dto)
        {
            return await _userRoleRepository.Select.ToPageResultAsync<UserRoleEntity,UserRoleOutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(UserRoleInputDto input)
        {
            var entity = input.Adapt<UserRoleEntity>();
            entity.Id = Snowflake.GenId();
            var result = await _userRoleRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(UserRoleInputDto input)
        {
            var data = await _userRoleRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }
            _userRoleRepository.Attach(roleModel);

                    data.Id = input.Id
                    
                    data.CreatedId = input.CreatedId
                    
                    data.CreateTime = input.CreateTime
                    
                    data.DeleteTime = input.DeleteTime
                    
                    data.RoleId = input.RoleId
                    
                    data.UserId = input.UserId
                                }
            int res = await _userRoleRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _userRoleRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
