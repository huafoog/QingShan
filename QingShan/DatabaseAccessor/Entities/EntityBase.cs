using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QingShan.DatabaseAccessor
{
    /// <summary>
    /// 实体类基类
    /// <para>只包含<see cref="IEntity{TKey}"/>接口类</para>
    /// </summary>
    public abstract class EntityBaseById<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        [DisplayName("编号")]
        public TKey Id { get; set; }
    }

    /// <summary>
    /// 实体基类
    /// <para>包含<see cref="IEntity{TKey}"/>|<see cref="ICreatedTime"/>|<see cref="IDataState"/>接口类</para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityBase<TKey> : IEntity<TKey>, ICreatedTime, ISoftDeletable
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        [DisplayName("编号")]
        [Key]
        public TKey Id { get; set; }
        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 获取或设置 数据状态
        /// </summary>
        [DisplayName("数据状态")]
        public DateTime? DeleteTime { get; set; }
    }
}
