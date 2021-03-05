namespace QingShan.DatabaseAccessor
{
    /// <summary>
    /// 无键实体基接口
    /// </summary>
    public interface IEntityNotKey:IEntity
    {
        /// <summary>
        /// 数据库中定义名
        /// </summary>
        string DEFINED_NAME { get; set; }
    }
}
