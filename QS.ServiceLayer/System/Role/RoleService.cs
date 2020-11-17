using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QS.Core.Data;
using QS.Core.Dependency;
using QS.Core.Permission;
using QS.DataLayer.Entities;
using QS.ServiceLayer.System.Role.Dto.InputDto;
using QS.ServiceLayer.System.Role.Dto.OutputDto;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QS.ServiceLayer.System.Role
{
    public class RoleService : IRoleService, IScopeDependency
    {
        private readonly IUserInfo _user;
        private readonly IMapper _mapper;
        private readonly EFContext _context;
        private readonly IConfigurationProvider _configurationProvider;
        /// <summary>
        /// 角色
        /// </summary>
        private IQueryable<RoleEntity> Roles => _context.GetDbSet<RoleEntity, int>().GetTrackEntities();

        public RoleService(IUserInfo user,
            IMapper mapper,
            EFContext context,
            IConfigurationProvider configurationProvider
            )
        {
            _user = user;
            _mapper = mapper;
            _context = context;
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult<RoleOutputDto>> GetAsync(int id)
        {
            var entityDto = await Roles.ProjectTo<RoleOutputDto>(_configurationProvider).FirstOrDefaultAsync(o => o.Id == id);
            return new StatusResult<RoleOutputDto>(entityDto);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<RoleOutputDto>> PageAsync(PageInputDto dto)
        {

            var data = await Roles.LoadPageListAsync(dto, u => new RoleOutputDto
            {
                CreatedTime = u.CreateTime,
                Id = u.Id,
                Description = u.Description,
                Name = u.Name,
                OrderSort = u.OrderSort,
                Enabled = u.Enabled
            }, o => dto.Search.IsNull() || o.Name.Contains(dto.Search) || o.Description.Contains(dto.Search));
            return data;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(RoleInputDto input)
        {
            if (Roles.Any(o => o.Name == input.Name))
            {
                return new StatusResult("角色名已存在");
            }
            var entity = _mapper.Map<RoleEntity>(input);
            var id = await _context.InsertEntityAsync<RoleEntity, int>(entity);
            if (id == 0)
            {
                return new StatusResult("添加失败");
            }
            //if (input.RoleIds != null && input.RoleIds.Any())
            //{
            //    var roles = input.RoleIds.Select(a => new UserRoleEntity { UserId = _user.Id, RoleId = a });
            //    await _context.InsertEntitiesAsync(roles.ToArray());
            //}
            return new StatusResult();
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

            var role = await Roles.FirstOrDefaultAsync(o => o.Id == input.Id);
            if (!(role?.Id > 0))
            {
                return new StatusResult("用户不存在！");
            }

            var roleModel = _mapper.Map(input, role);

            Expression<Func<RoleEntity, object>>[] updatedProperties = {
                    p => p.Name,
                    p => p.Description,
                    p => p.OrderSort,
                    p => p.Enabled
                };
            int res = await _context.UpdateEntity<RoleEntity, int>(roleModel, updatedProperties);
            return new StatusResult(res > 0, "修改失败");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(int id)
        {
            var res = await _context.Set<RoleEntity>().DeleteByIdAsync(id);
            return new StatusResult(res > 0, "删除失败");
        }
    }
}
