/* ========================================================		
 * Module Name: JuCheap.Core.Email
 * Class  Name: FTMailBase
 * Description: 所有邮件Sender的基类
 * Company    : www.jucheap.com
 * Author     : dj.wong
 * Create Date: 2014-11-03
===========================================================*/

using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using JuCheap.Core.Extentions;
using JuCheap.Core.Log;

namespace JuCheap.Core.Email
{
    internal class SMTPMailer : FTMailBase
    {
        /// <summary>
        /// SMTP邮件Sender的类的封装
        /// </summary>
        /// <param name="smtpServer">smtp服务地址</param>
        /// <param name="port">端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="enableSSL">是否采用ssl的方式</param>
        public override bool Send(string smtpServer, int port, string userName, string password,bool enableSSL)
        {
            bool isSended = false;
            try
            {
                MailMessage message = new MailMessage();
                To.ForEach(email =>
                {
                    if (email.IsValidEmail())
                        message.To.Add(email);
                });
                message.Sender = new MailAddress(userName, userName);
                if (CC != null && CC.Any())
                    CC.ForEach(email =>
                    {
                        if (email.IsValidEmail())
                            message.CC.Add(email);
                    });
                message.Subject = Encoding.Default.GetString(Encoding.Default.GetBytes(Subject));

                message.From = DisplayName.IsBlank() ? new MailAddress(From) : new MailAddress(From, Encoding.Default.GetString(Encoding.Default.GetBytes(DisplayName)));

                message.Body = Encoding.Default.GetString(Encoding.Default.GetBytes(Body));
                message.BodyEncoding = Encoding.GetEncoding("GBK") ;
                message.HeadersEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                message.ReplyToList.Add(new MailAddress(From));

                SmtpClient smtp = new SmtpClient(smtpServer, port)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(userName, password),
                    Timeout = 30000,
                    EnableSsl = enableSSL
                };

                smtp.SendCompleted += smtp_SendCompleted;
                smtp.Send(message);
                isSended = true;
            }
            catch (Exception ex)
            {
                Logger.Log("邮件发送失败：", ex);
            }
            return isSended;
        }
        /// <summary>
        /// 发送完成后，如果有错误，记录日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Logger.Log("邮件发送失败(smtp_SendCompleted)：", e.Error);
            }
        }
    }
}
