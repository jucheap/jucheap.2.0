using JuCheap.Entity.Base;

namespace JuCheap.Entity
{
    /// <summary>
    /// 邮件接收人
    /// </summary>
    public class EmailReceiverEntity : BaseEntity
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        public int EmailId { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 类型:收件人/抄送人/密送人 ext;
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public virtual  EmailPoolEntity EmailPool { get; set; }
    }
}
