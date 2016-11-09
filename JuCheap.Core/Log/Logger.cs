/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 09/04/2015 11:47:14
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.IO;
using log4net;

namespace JuCheap.Core.Log
{
    /// <summary>
    /// 通用日志记录器
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 通用错误日志记录器
        /// </summary>
        private static readonly ILog Logger_Error = LogManager.GetLogger("logerror");

        /// <summary>
        /// 通用信息日志记录器
        /// </summary>
        private static readonly ILog Logger_Info = LogManager.GetLogger("loginfo");

        /// <summary>
        /// 
        /// </summary>
        static Logger()
        {
            FileInfo log4netFile =
                new FileInfo(string.Format("{0}/Config/log4net.config", AppDomain.CurrentDomain.BaseDirectory));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(log4netFile);
        }

        /// <summary>
        /// 记录日常日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Log(string message, Exception ex)
        {
            Logger_Error.Error(message, ex);
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message"></param>
        public static void LogInfo(string message)
        {
            Logger_Error.Error(message);
        }
    }
}
