using QS.DatabaseAccessor;
using QS.Permission;
using System;

namespace QS.Extensions
{
    /// <summary>
    /// 实体接口扩展方法
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// 检测指定类型是否为<see cref="IEntity{TKey}"/>实体类型
        /// </summary>
        /// <param name="type">要判断的类型</param>
        /// <returns></returns>
        public static bool IsEntityType(this Type type)
        {
            return typeof(IEntity<>).IsGenericAssignableFrom(type) && !type.IsAbstract && !type.IsInterface;
        }

        /// <summary>
        /// 判断指定实体是否已过期
        /// </summary>
        /// <param name="entity">要检测的实体</param>
        /// <returns></returns>
        public static bool IsExpired(this IExpirable entity)
        {
            DateTime now = DateTime.Now;
            return entity.BeginTime != null && entity.BeginTime.Value > now || entity.EndTime != null && entity.EndTime.Value < now;
        }

        /// <summary>
        /// 检测并执行<see cref="ICreatedTime"/>接口的逻辑
        /// </summary>
        public static TEntity CheckICreatedTime<TEntity, TKey>(this TEntity entity)
            where TEntity : IEntity
        {
            if (!(entity is ICreatedTime))
            {
                return entity;
            }
            ICreatedTime entity1 = (ICreatedTime)entity;
            if (entity1.CreateTime == default(DateTime))
            {
                entity1.CreateTime = DateTime.Now;
            }

            return (TEntity)entity1;
        }

        /// <summary>
        /// 检测并执行<see cref="IDateleTime"/>接口的逻辑
        /// </summary>
        public static TEntity CheckIDateleTime<TEntity, TKey>(this TEntity entity)
            where TEntity : IEntity
            where TKey : IEquatable<TKey>
        {
            if (!(entity is ISoftDeletable))
            {
                return entity;
            }
            ISoftDeletable entity1 = (ISoftDeletable)entity;
            if (entity1.DeleteTime == default(DateTime))
            {
                entity1.DeleteTime = DateTime.Now;
            }

            return (TEntity)entity1;
        }


        /// <summary>
        /// 检测并执行<see cref="ICreationAudited{TUserKey}"/>接口的处理
        /// </summary>
        public static TEntity CheckICreationAudited<TEntity, TKey>(this TEntity entity, IUserInfo user)
            where TEntity : IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            if (!(entity is ICreationAudited<int>))
            {
                return entity;
            }

            ICreationAudited<int> entity1 = (ICreationAudited<int>)entity;
            entity1.CreatorId = user.Id;
            entity1.CreateTime = DateTime.Now;
            return (TEntity)entity1;
        }

        /// <summary>
        /// 检测并执行<see cref="IUpdateAudited{TUserKey}"/>接口的处理
        /// </summary>
        public static TEntity CheckIUpdateAudited<TEntity, TKey>(this TEntity entity, IUserInfo user)
            where TEntity : IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            if (!(entity is IUpdateAudited<int>))
            {
                return entity;
            }

            IUpdateAudited<int> entity1 = (IUpdateAudited<int>)entity;
            entity1.LastUpdaterId = user.Id;
            entity1.LastUpdatedTime = DateTime.Now;
            return (TEntity)entity1;
        }
    }
}
