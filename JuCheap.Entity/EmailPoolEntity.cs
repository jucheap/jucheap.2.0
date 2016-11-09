using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuCheap.Entity.Base;

namespace JuCheap.Entity
{
    public class EmailPoolEntity : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送失败的次数
        /// </summary>
        public int FailTimes { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 预计发送时间
        /// </summary>
        public DateTime? PreSendTime { get; set; }

        /// <summary>
        /// 实际发送时间
        /// </summary>
        public DateTime? ActualSendTime { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public virtual ICollection<EmailReceiverEntity> Receivers { get; set; } 
    }
}
