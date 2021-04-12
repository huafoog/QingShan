using FreeSql;
using Mapster;
using QingShan.Data;
using QingShan.DatabaseAccessor;
using QingShan.DependencyInjection;
using QingShan.Encryption;
using QingShan.Permission;
using QingShan.DataLayer.Entities;
using QingShan.Services.User.Dtos.InputDto;
using QingShan.Services.User.Dtos.OutputDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QingShan.Services.User
{
    /// <summary>
    /// 用户管理-
    /// </summary>
    public class UserService : IUserService, IScopeDependency
    {
        private readonly IUserInfo _user;

        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IRepository<UserEntity, int> _userRepository;

        public UserService(IUserInfo user,
            IRepository<UserEntity, int> userRepository
            )
        {
            _user = user;
            _userRepository = userRepository;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult<UserGetOutputDto>> GetAsync(int id)
        {
            var user = await _userRepository
                .Select
                .Where(o=>o.Id == id)
                .FirstAsync<UserGetOutputDto>();



            //var entity = await _userRepository.Select
            //.WhereDynamic(id)
            //.IncludeMany(a => a.Roles.Select(b => new RoleEntity { Id = b.Id }))
            //.ToOneAsync();

            //var entityDto = await _context.Users.ProjectTo<UserGetOutputDto>(_configurationProvider).FirstOrDefaultAsync(o => o.Id == id);
            //var entityDto = _mapper.Map<UserGetOutputDto>(entity);
            return new StatusResult<UserGetOutputDto>(user);
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <returns></returns>
        public async Task<IList<string>> GetPermissionsAsync()
        {
            await Task.CompletedTask;
            //var key = string.Format(CacheKey.UserPermissions, _user.Id);
            //if (await _cache.ExistsAsync(key))
            //{
            //    return await _cache.GetAsync<IList<string>>(key);
            //}
            //else
            //{

            //}
            //这里加缓存

            //var userPermissoins = await (from rp in _userRepository.Select.RoleModules
            //                             join ur in _context.UserRole on rp.RoleId equals ur.RoleId
            //                             join p in _context.Modules on rp.ModuleId equals p.Id
            //                             select p.Id.ToString()).ToListAsync();
            //return userPermissoins;
            return new List<string>();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageOutputDto<UserListOutputDto>> PageAsync(PageInputDto dto)
        {
            var data = await _userRepository
                .Select
                .ToPageResultAsync<UserEntity,UserListOutputDto>(
                    dto, 
                    o => dto.Search.IsNull() || o.UserName.Contains(dto.Search) || o.NickName.Contains(dto.Search)
                );
            return data;


        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> AddAsync(UserAddInputDto input)
        {
            if (input.Password.IsNull())
            {
                input.Password = "123456"; //初始密码 123456
            }

            if (_userRepository.Select.Any(o => o.UserName == input.UserName))
            {
                return new StatusResult("账号已存在");
            }

            input.Password = MD5Encrypt.Encrypt32(input.Password);
            var entity = input.Adapt<UserEntity>();
            var res = await _userRepository.InsertOrUpdateAsync(entity);
            return new StatusResult(res != null, "添加失败");
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<StatusResult> UpdateAsync(UserUpdateInputDto input)
        {
            if (!(input?.Id > 0))
            {
                return new StatusResult("未获取到用户信息");
            }

            var user = await _userRepository.Select.Where(o => o.Id == input.Id).FirstAsync();
            if (!(user?.Id > 0))
            {
                return new StatusResult("用户不存在！");
            }
            var thisUser = new UserEntity()
            {
                Id = input.Id
            };
            _userRepository.Attach(thisUser);
            thisUser.Avatar = input.Avatar;
            thisUser.NickName = input.NickName;
            thisUser.Phone = input.Phone;
            thisUser.RealName = input.RealName;
            int res = await _userRepository.UpdateAsync(thisUser);
            return new StatusResult(res > 0, "修改失败");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult> DeleteAsync(int id)
        {
            var res = await _userRepository.DeleteAsync(id);
            return new StatusResult(res > 0, "删除失败");
        }
    }
}
