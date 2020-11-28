using FreeSql;
using Mapster;
using QS.Core.Data;
using QS.Core.DatabaseAccessor;
using QS.Core.DependencyInjection;
using QS.Core.Permission;
using QS.DataLayer.Entities;
using QS.ServiceLayer.System.Role.Dto.InputDto;
using QS.ServiceLayer.System.Role.Dto.OutputDto;
using System;
using System.Threading.Tasks;

namespace QS.ServiceLayer.System.Role
{
    public class RoleService : IRoleService, IScopeDependency
    {
        private readonly IUserInfo _user;
        private readonly IRepository<RoleEntity, int> _roleRepository;

        public RoleService(IUserInfo user,
            IRepository<RoleEntity,int> roleRepository
            )
        {
            _user = user;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult<RoleOutputDto>> GetAsync(int id)
        {
            var entityDto = await _roleRepository.Select.Where(o => o.Id.Equals(id)).FirstAsync<RoleOutputDto>();
            return new StatusResult<RoleOutputDto>(entityDto);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<RoleOutputDto>> PageAsync(PageInputDto dto)
        {

            return await _roleRepository.Select.ToPageResultAsync<RoleEntity,RoleOutputDto>(dto
                , o => dto.Search.IsNull()  || o.Name.Contains(dto.Search)  || o.Description.Contains(dto.Search)
            );
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(RoleInputDto input)
        {
            if (_roleRepository.Select.Any(o => o.Name == input.Name))
            {
                return new StatusResult("角色名已存在");
            }
            var entity = input.Adapt<RoleEntity>();
            var result = await _roleRepository.InsertAsync(entity);
            return new StatusResult(result != null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(RoleInputDto input)
        {
            if (!(input?.Id > 0))
            {
                return new StatusResult("未获取到用户信息");
            }

            var role = await _roleRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (!(role?.Id > 0))
            {
                return new StatusResult("用户不存在！");
            }

            var roleModel = new RoleEntity() { 
                Id = input.Id
            };
            _roleRepository.Attach(roleModel);
            roleModel.Name = input.Name;
            roleModel.Description=input.Description;
            roleModel.OrderSort=input.OrderSort;
            roleModel.Enabled=input.Enabled;
            int res = await _roleRepository.UpdateAsync(roleModel);
            return new StatusResult(res > 0, "修改失败");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(int id)
        {
            var res = await _roleRepository.DeleteAsync(id);
            return new StatusResult(res > 0, "删除失败");
        }
    }
}
