
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JuCheap.Service.Enum;

namespace JuCheap.Service.Dto
{
    /// <summary>
    /// 用户DTO
    /// </summary>
    public class UserDto : BaseDto
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        [DisplayName("登录账号*"), Required, StringLength(20, MinimumLength = 5, ErrorMessage = "长度在5-20个字符之间")]
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [DisplayName("真实姓名*"), Required, StringLength(20, MinimumLength = 2, ErrorMessage = "长度在2-20个字符之间")]
        public string RealName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [DisplayName("登录密码*"), Required, StringLength(36, MinimumLength = 6, ErrorMessage = "长度在6-36个字符之间")]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱*"), Required, StringLength(36, MinimumLength = 5, ErrorMessage = "长度在5-36个字符之间")]
        public string Email { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [DisplayName("用户状态*"), Required]
        public UserStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get { return Status.ToString(); }
        }

        /// <summary>
        /// 记住账号
        /// </summary>
        public bool IsRememberMe { get; set; }
    }
}
