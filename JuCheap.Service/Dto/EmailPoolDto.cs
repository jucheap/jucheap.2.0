using System;
using System.Collections.Generic;
using System.ComponentModel;
using JuCheap.Service.Enum;

namespace JuCheap.Service.Dto
{
    public class EmailPoolDto : BaseDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DisplayName("邮件标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DisplayName("邮件内容")]
        public string Content { get; set; }

        /// <summary>
        /// 发送失败的次数
        /// </summary>
        public int FailTimes { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EmailStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName {
            get { return Status.ToString(); }
        }

        /// <summary>
        /// 预计发送时间
        /// </summary>
        public DateTime? PreSendTime { get; set; }

        /// <summary>
        /// 实际发送时间
        /// </summary>
        public DateTime? ActualSendTime { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public List<EmailReceiverDto> Receivers { get; set; }

        [DisplayName("收件人")]
        public  string ReceiverEmails { get; set; }
    }
}
