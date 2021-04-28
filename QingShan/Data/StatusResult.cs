namespace QingShan.Data
{
    /// <summary>
    /// 业务操作信息信息类
    /// </summary>
    public class StatusResult : StatusResult<object>
    {
        public StatusResult()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFail">是否失败</param>
        /// <param name="message">失败显示的消息</param>
        public StatusResult(bool isFail, string message) : base(!isFail, message)
        {

        }

        /// <summary>
        /// 设置错误消息
        /// </summary>
        /// <param name="message"></param>
        public StatusResult(string message) : base(StatusResultType.Error, message)
        {
        }
    }
    public class StatusResult<TData> : IResponseOutput<TData>
    {
        public StatusResult()
        {
            IsSuccess = true;
        }

        public StatusResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            ResultType = isSuccess ? StatusResultType.Success : StatusResultType.Error;
            Message = isSuccess ? "" : message;
        }

        public StatusResult(string message) : this(StatusResultType.Error, message)
        {
        }

        public StatusResult(TData data) : this(StatusResultType.Success, "", data)
        {
        }

        public StatusResult(StatusResultType resultType, string message = "", TData data = default(TData))
        {
            IsSuccess = resultType == StatusResultType.Success ? true : false;
            Data = data;
            ResultType = resultType;
            Message = message;
        }

        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        public StatusResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功    
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        public TData Data { get; set; }
    }
}
