using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using QS.Core.Data;
using QS.Core.Dependency;
using QS.Core.Encryption;
using QS.Core.Extensions;
using QS.Core.Permission;
using QS.DataLayer.Entities;
using QS.ServiceLayer.User.Dtos.InputDto;
using QS.ServiceLayer.User.Dtos.OutputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.User
{
    /// <summary>
    /// 用户管理-
    /// </summary>
    public class UserService : IUserService, IScopeDependency
    {
        private readonly IUserInfo _user;
        private readonly IMapper _mapper;
        private readonly EFContext _context;
        private readonly IConfigurationProvider _configurationProvider;
        /// <summary>
        /// 用户信息
        /// </summary>
        private IQueryable<UserEntity> Users => _context.Users.GetTrackEntities();

        public UserService(IUserInfo user,
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
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StatusResult<UserGetOutputDto>> GetAsync(int id)
        {

            //var entity = await _userRepository.Select
            //.WhereDynamic(id)
            //.IncludeMany(a => a.Roles.Select(b => new RoleEntity { Id = b.Id }))
            //.ToOneAsync();

            var entityDto = await _context.Users.ProjectTo<UserGetOutputDto>(_configurationProvider).FirstOrDefaultAsync(o => o.Id == id);
            //var entityDto = _mapper.Map<UserGetOutputDto>(entity);
            return new StatusResult<UserGetOutputDto>(entityDto);
        }
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <returns></returns>
        public async Task<StatusResult<UserGetOutputDto>> GetBasicAsync()
        {
            if (!(_user?.Id > 0))
            {
                return new StatusResult<UserGetOutputDto>("未登录！");
            }

            var data = await Users.ProjectTo<UserGetOutputDto>(_configurationProvider).FirstOrDefaultAsync(o => o.Id == _user.Id);

            return new StatusResult<UserGetOutputDto>(data);
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <returns></returns>
        public async Task<IList<string>> GetPermissionsAsync()
        {
            //var key = string.Format(CacheKey.UserPermissions, _user.Id);
            //if (await _cache.ExistsAsync(key))
            //{
            //    return await _cache.GetAsync<IList<string>>(key);
            //}
            //else
            //{

            //}
            //这里加缓存

            var userPermissoins = await (from rp in _context.RolePermissions
                                         join ur in _context.UserRole on rp.RoleId equals ur.RoleId
                                         join p in _context.Modules on rp.PermissionId equals p.Id
                                         select p.Code).ToListAsync();
            return userPermissoins;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<StatusResult<PageOutputDto<UserListOutputDto>>> PageAsync(PageInputDto dto)
        {
            var data = await (from u in Users
                       select new UserListOutputDto
                       {
                           CreatedTime = u.CreateTime,
                           Id = u.Id,
                           Name = u.RealName,
                           NickName = u.NickName,
                           RoleNames = (from ur in _context.UserRole
                                        join r in _context.Roles on ur.RoleId equals r.Id
                                        where ur.UserId == u.Id
                                        select r.Name).ToArray(),
                       }).LoadPageListAsync(dto);
            return new StatusResult<PageOutputDto<UserListOutputDto>>(data);
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
                //初始密码 123456
                input.Password = "123456";
            }

            input.Password = MD5Encrypt.Encrypt32(input.Password);
            var tran = await _context.Database.BeginTransactionAsync();
            try
            {

                var entity = _mapper.Map<UserEntity>(input);
                var id = await _context.InsertEntityAsync<UserEntity, int>(entity);
                if (id == 0)
                {
                    return new StatusResult("添加失败");
                }

                if (input.RoleIds != null && input.RoleIds.Any())
                {
                    var roles = input.RoleIds.Select(a => new UserRoleEntity { UserId = _user.Id, RoleId = a });
                    await _context.InsertEntitiesAsync(roles.ToArray());
                }
                await tran.CommitAsync();
                return new StatusResult();
            }
            catch (Exception)
            {
                await tran.RollbackAsync();
                throw;
            }
        }

          public async Task<StatusResult> UpdateAsync(UserUpdateInputDto input)
          {
              if (!(input?.Id > 0))
              {
                  return new StatusResult("未获取到用户信息");
              }

              var user = await Users.FirstOrDefaultAsync(o=>o.Id == input.Id);
              if (!(user?.Id > 0))
              {
                  return new StatusResult("用户不存在！");
              }

              var users = _mapper.Map(input, user);
              _context.Users.Update(users);
              await _context.UserRole.Where(a => a.UserId == user.Id).BatchDeleteAsync();
              if (input.RoleIds != null && input.RoleIds.Any())
              {
                  var roles = input.RoleIds.Select(a => new UserRoleEntity { UserId = user.Id, RoleId = a });
                  await _context.UserRole.AddRangeAsync(roles);
              }
              return new StatusResult();
          }
        /*
          public async Task<StatusResult> UpdateBasicAsync(UserUpdateBasicInput input)
          {
              var entity = await _userRepository.GetAsync(input.Id);
              entity = _mapper.Map(input, entity);
              var result = (await _userRepository.UpdateAsync(entity)) > 0;

              return StatusResult.Result(result);
          }

          public async Task<StatusResult> ChangePasswordAsync(UserChangePasswordInput input)
          {
              if (input.ConfirmPassword != input.NewPassword)
              {
                  return StatusResult.NotOk("新密码和确认密码不一致！");
              }

              var entity = await _userRepository.GetAsync(input.Id);
              var oldPassword = MD5Encrypt.Encrypt32(input.OldPassword);
              if (oldPassword != entity.Password)
              {
                  return StatusResult.NotOk("旧密码不正确！");
              }

              input.Password = MD5Encrypt.Encrypt32(input.NewPassword);

              entity = _mapper.Map(input, entity);
              var result = (await _userRepository.UpdateAsync(entity)) > 0;

              return StatusResult.Result(result);
          }

          public async Task<StatusResult> DeleteAsync(long id)
          {
              var result = false;
              if (id > 0)
              {
                  result = (await _userRepository.DeleteAsync(m => m.Id == id)) > 0;
              }

              return StatusResult.Result(result);
          }

          public async Task<StatusResult> SoftDeleteAsync(long id)
          {
              var result = await _userRepository.SoftDeleteAsync(id);

              return StatusResult.Result(result);
          }

          public async Task<StatusResult> BatchSoftDeleteAsync(long[] ids)
          {
              var result = await _userRepository.SoftDeleteAsync(ids);

              return StatusResult.Result(result);
          }*/
    }
}
