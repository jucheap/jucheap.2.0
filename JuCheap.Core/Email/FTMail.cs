/* ========================================================		
 * Module Name: JuCheap.Core.Email
 * Class  Name: FTMailBase
 * Description: 所有邮件Sender的基类
 * Company    : www.jucheap.com
 * Author     : dj.wong
 * Create Date: 2014-11-03
===========================================================*/

using System.Collections.Generic;

namespace JuCheap.Core.Email
{
    /// <summary>
    /// 所有邮件Sender的基类
    /// </summary>
    public abstract class FTMailBase
    {
        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 收件人列表
        /// </summary>
        public List<string> To { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        //正文
        public string Body { get; set; }
        /// <summary>
        /// 抄送人列表
        /// </summary>
        public List<string> CC { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="smtpServer">邮件服务器地址</param>
        /// <param name="port">端口</param>
        /// <param name="userName">帐号</param>
        /// <param name="password">密码</param>
        /// <param name="enableSSL">是否启用SSL</param>
        /// <returns></returns>
        public abstract bool Send(string smtpServer
                         , int port
                         , string userName
                         , string password
                         , bool enableSSL);

        /// <summary>
        /// 工厂方法，获取邮件SENDER实例
        /// </summary>
        public static FTMailBase Instance
        {
            get
            {
                return new SMTPMailer();
            }
        }
    }
}
