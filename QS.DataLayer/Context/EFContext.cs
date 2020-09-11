using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using MySqlConnector.Logging;
using QS.Core.Attributes;
using QS.Core.Entity;
using QS.DataLayer.Entities.Configuration;
using QS.DataLayer.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public DbSet<ModuleEntity> Modules { get; set; }

        public DbSet<RoleModuleEntity> RoleModules { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public DbSet<UserRoleEntity> UserRole { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<RoleEntity> Roles { get; set; }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #if Debug
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EFLoggerProvider());
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseLoggerFactory(loggerFactory);
            #endif


            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        #region 异步方法

        /// <summary>
        /// 获取DbSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public virtual DbSet<T> GetDbSet<T, TKey>() where T:class,IEntity<TKey> => Set<T>();

        /// <summary>
        /// 更新部分字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="entity"></param>
        /// <param name="updatedProperties"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateEntity<T,TKey>(T entity, Expression<Func<T, object>>[] updatedProperties)
            where T: class,IEntity<TKey>
        {
            Set<T>().Attach(entity);
            if (updatedProperties.Any())
            {
                foreach (var property in updatedProperties)
                {
                    Entry(entity).Property(property).IsModified = true;
                }
            }
            return await SaveChangesAsync();
        }
        #endregion
    }


}
