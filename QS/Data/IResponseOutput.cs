using System.Text.Json.Serialization;

namespace QS.Data
{
    /// <summary>
    /// 响应数据输出接口
    /// </summary>
    public interface IResponseOutput : IResponseOutput<object>
    {
    }

    /// <summary>
    /// 响应数据输出泛型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseOutput<T>
    {
        /// <summary>
        /// 结果状态
        /// </summary>
        StatusResultType ResultType { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        T Data { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonIgnore]
        bool IsSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        string Message { get; set; }
    }
}
