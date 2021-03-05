namespace QS.DatabaseAccessor
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey">主键id</typeparam>
    public interface IEntity<TKey>: IEntity
    {
        /// <summary>
        /// 获取或设置 编号
        /// </summary>
        TKey Id { get; set; }
    }
    public interface IEntity
    {
    }

}
