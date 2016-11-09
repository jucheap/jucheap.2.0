/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2015/8/21
* Description: Automated building by service@jucheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using JuCheap.Core.Log;

namespace JuCheap.Core
{
    /// <summary>
    /// web中的一些工具类
    /// </summary>
    public class WebHelper
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(int dest, int host, ref long mac, ref int length);
        [DllImport("Ws2_32.dll")]
        private static extern int inet_addr(string ip);

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        /// <summary>
        /// 获取客户端MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientMACAddress()
        {

            string mac_dest = string.Empty;

            try
            {
                string strClientIP = HttpContext.Current.Request.UserHostAddress.Trim();
                int ldest = inet_addr(strClientIP); //目的地的ip 
                inet_addr("");//本地服务器的ip 
                long macinfo = new long();
                int len = 6;
                SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");

                if (mac_src != "0")
                {
                    while (mac_src.Length < 12)
                    {
                        mac_src = mac_src.Insert(0, "0");
                    }

                    for (int i = 0; i < 11; i++)
                    {
                        if (0 == (i % 2))
                        {
                            mac_dest = i == 10
                                ? mac_dest.Insert(0, mac_src.Substring(i, 2))
                                : "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
            }
            catch
            {
                //igone
            }
            return mac_dest;
        }
        /// <summary>
        /// 判断是否为AJAX请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsXHR(HttpRequestBase request)
        {
            bool ret = false;
            if (request != null)
            {

                if (request.Headers["X-Requested-With"] != null && request.Headers["X-Requested-With"].Trim().Length > 0)
                {
                    ret = true;
                }
            }
            return ret;
        }

        /// <summary>
        /// 发送电子邮件 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="address"></param>
        /// <param name="pwd"></param>
        /// <param name="MessageFrom"></param>
        /// <param name="MessageTo"></param>
        /// <param name="MessageSubject"></param>
        /// <param name="MessageBody"></param>
        /// <returns></returns>
        public static bool Send(string host, string address, string pwd, MailAddress MessageFrom, string MessageTo, string MessageSubject, string MessageBody)
        {
            MailMessage message = new MailMessage();
            string Host = host;
            string Mail_Address = address;
            string Mail_Pwd = pwd;


            message.From = MessageFrom;
            message.To.Add(MessageTo); //收件人邮箱地址可以是多个以实现群发 
            message.Subject = MessageSubject;
            message.Body = MessageBody;
            message.IsBodyHtml = false; //是否为html格式 
            message.Priority = MailPriority.High; //发送邮件的优先等级 

            SmtpClient sc = new SmtpClient
            {
                Host = Host,
                Port = 25,
                Credentials = new NetworkCredential(Mail_Address, Mail_Pwd)
            };
            //指定发送邮件的服务器地址或IP 
            //指定发送邮件端口 
            //指定登录服务器的用户名和密码 

            try
            {
                sc.Send(message); //发送邮件
            }
            catch(Exception ex)
            {
                Logger.Log("发送邮件失败", ex);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 构建自动提交的支付表单页面的HTML
        /// </summary>
        /// <param name="formName">表单名称</param>
        /// <param name="actionUrl">地址</param>
        /// <param name="formType">方式(get;post)</param>
        /// <param name="keyValues">键值对数据</param>
        /// <returns></returns>
        public static string BuildForm(string formName, string actionUrl, string formType, IDictionary<string, string> keyValues)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<form id=\"{0}\" name=\"{0}\" action=\"{1}\" method=\"{2}\">", formName, actionUrl, formType);
            foreach (KeyValuePair<string, string> kp in keyValues)
            {
                sb.AppendFormat("<input type=\"hidden\" name=\"{0}\"  id=\"{0}\" value=\"{1}\"  />", kp.Key, kp.Value);
            }
            sb.AppendFormat("</form>");
            sb.AppendFormat("<script>document.forms['{0}'].submit();</script>", formName);
            return sb.ToString();
        }
    }
}
