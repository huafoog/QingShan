﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QS.DataLayer.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QS.DataLayer.Entities
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        {
            //如果有数据库存在，那么什么也不会发生。但是如果没有，那么就会创建一个数据库。
            //Database.EnsureCreated();
            //if (Database.GetPendingMigrations().Any())
            //{
            //    //数据库迁移
            //    Database.Migrate();
            //}
            Database.Migrate();
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<FunctionEntity> Functions { get; set; }

        public DbSet<ModuleEntity> Modules { get; set; }

        public DbSet<RolePermissionEntity> RolePermissions { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public DbSet<UserRoleEntity> UserRole { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        /// <summary>
        /// 功能权限
        /// </summary>
        public DbSet<ModuleFunctionEntity> ModuleFunction { get; set; }


        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        /// <summary>
        /// 重写添加方法
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {




            return base.AddAsync(entity, cancellationToken);
        }


        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            return base.Remove(entity);
        }
    }
}
