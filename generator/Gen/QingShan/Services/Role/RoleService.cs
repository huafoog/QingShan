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

using QingShan.Services.Role;
using QingShan.Services.Role.Dto;
using QingShan.DataLayer.Entities;
namespace QingShan.Services.Role
{
    /// <summary>
	/// 角色模型
    /// </summary>
    public class RoleService:IRoleContract,IScopeDependency
    {
        private readonly IRepository<RoleEntity> _roleRepository;
        public RoleService(IRepository<RoleEntity> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<RoleOutputDto>> PageAsync(PageRoleInputDto dto)
        {
            return await _roleRepository.Select.ToPageResultAsync<RoleEntity,RoleOutputDto>(dto,null);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(RoleInputDto input)
        {
            var entity = input.Adapt<RoleEntity>();
            entity.Id = Snowflake.GenId();
            var result = await _roleRepository.InsertAsync(entity);
            return new StatusResult(result == null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(RoleInputDto input)
        {
            var data = await _roleRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (data == null)
            {
                return new StatusResult("数据不存在！");
            }
            _roleRepository.Attach(roleModel);

                    data.Id = input.Id
                    
                    data.CreatedId = input.CreatedId
                    
                    data.CreateTime = input.CreateTime
                    
                    data.DeleteTime = input.DeleteTime
                    
                    data.Description = input.Description
                    
                    data.Enabled = input.Enabled
                    
                    data.Name = input.Name
                    
                    data.OrderSort = input.OrderSort
                                }
            int res = await _roleRepository.UpdateAsync(data);
            return new StatusResult(res == 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(string id)
        {
            var res = await _roleRepository.DeleteAsync(id);
            return new StatusResult(res == 0, "删除失败");
        }

    }
}
