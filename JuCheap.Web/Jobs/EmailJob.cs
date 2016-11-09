using System;
using System.Linq;
using JuCheap.Core.Email;
using JuCheap.Core.Log;
using JuCheap.Service.Abstracts;
using JuCheap.Service.Config;
using JuCheap.Service.Enum;
using Quartz;

namespace JuCheap.Web.Jobs
{
    /// <summary>
    /// 邮件发送Job
    /// </summary>
    public class EmailJob : IJob
    {
        public IEmailPoolService emailPoolService { get; set; }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var smtp = FTMailBase.Instance;
            var setting = EmailConfiguration.Setting;
            smtp.From = setting.Email;
            var list = emailPoolService.GetWithReceivers(10);
            if (list != null && list.Any())
            {
                list.ForEach(item =>
                {
                    try
                    {
                        var receivers =
                            item.Receivers.Where(r => r.Type == EmailReceiverType.收件人).Select(r => r.Email).ToList();
                        var ccs =
                            item.Receivers.Where(r => r.Type == EmailReceiverType.抄送人).Select(r => r.Email).ToList();
                        smtp.To = receivers;
                        smtp.CC = ccs;
                        smtp.Subject = item.Title;
                        smtp.Body = item.Content;
                        var flag = smtp.Send(setting.SmtpServer, setting.Port, setting.Email, setting.Password, false);
                        if (flag)
                        {
                            item.Status = EmailStatus.已发送;
                            item.ActualSendTime = DateTime.Now;
                        }
                        else
                        {
                            item.FailTimes += 1;
                            if (item.FailTimes >= 3)//发送失败次数，就不再发送；
                                item.Status = EmailStatus.发送失败;
                        }
                        emailPoolService.Update(item);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("定时任务发送邮件失败", ex);
                    }
                });
            }
        }
    }
}