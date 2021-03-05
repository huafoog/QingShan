namespace QingShan.Services.Account.Dto.OutputDto
{
    public class AuthLoginOutputDto
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string NickName { get; set; }
    }
}
