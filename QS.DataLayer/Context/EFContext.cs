using Microsoft.EntityFrameworkCore;
using QS.DataLayer.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.DataLayer.Entities
{
    public class EFContext:DbContext
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

        }


        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
