
namespace JuCheap.Service.Dto
{
    /// <summary>
    /// 用户登录日志DTO
    /// </summary>
    public class LoginLogDto : BaseDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 电脑的MAC地址
        /// </summary>
        public string Mac { get; set; }
    }
}
