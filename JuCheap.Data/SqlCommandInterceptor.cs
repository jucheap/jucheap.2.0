/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2016/2/24
* Description: Automated building by service@jucheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using JuCheap.Core.Config;

namespace JuCheap.Data
{
    /// <summary>
    /// SQL命令拦截器
    /// 主要实现EF的读写分离
    /// </summary>
    public class SqlCommandInterceptor : DbCommandInterceptor
    {
        #region Private Methods
        /// <summary>
        /// 处理读库字符串
        /// </summary>
        /// <returns></returns>
        private string GetReadConn()
        {
            var section = ReadDatabaseConfig.Connections;
            if (section == null || section.Databases == null || section.Databases.Count <= 0)
                return string.Empty;
            var seed = Guid.NewGuid().GetHashCode();
            var index = (double)new Random(seed).Next(0, section.Databases.Count);
            var resultConn = section.Databases[Convert.ToInt32(Math.Floor(index))];
            return string.Format(ConfigurationManager.AppSettings["readDbConnectioin"]
                , resultConn.Datasource
                , resultConn.DatabaseName
                , resultConn.User
                , resultConn.Password);
        }

        /// <summary>
        /// 只读库的选择,加工command对象
        /// </summary>
        /// <param name="command"></param>
        private void ReadDbSelect(DbCommand command)
        {
            var cmdText = command.CommandText.ToLower();
            var isWrite = cmdText.Contains("insert");
            var isMerge = cmdText.Contains("migrationhistory");

            if (isWrite || isMerge) return;
            if (string.IsNullOrWhiteSpace(GetReadConn())) return;

            command.Connection.Close();
            command.Connection.ConnectionString = GetReadConn();
            command.Connection.Open();
        }
        #endregion

        #region Override Methods

        /// <summary>
        /// Linq to Entity生成的update,delete
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            WriteDebug(command);
            base.NonQueryExecuting(command, interceptionContext);//update,delete等写操作直接走主库
        }
        /// <summary>
        /// 执行sql语句，并返回第一行第一列，没有找到返回null,如果数据库中值为null，则返回 DBNull.Value
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            WriteDebug(command);
            ReadDbSelect(command);
            base.ScalarExecuting(command, interceptionContext);
        }
        /// <summary>
        /// Linq to Entity生成的select,insert
        /// 发送到sqlserver之前触发
        /// warning:在select语句中DbCommand.Transaction为null，而ef会为每个insert添加一个DbCommand.Transaction进行包裹
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            WriteDebug(command);
            ReadDbSelect(command);
            base.ReaderExecuted(command, interceptionContext);
        }
        /// <summary>
        /// 发送到sqlserver之后触发
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            //WriteDebug(command);
            base.ReaderExecuted(command, interceptionContext);
        }

        void WriteDebug(DbCommand command)
        {
#if DEBUG
            string cmdText = command.CommandText;
            foreach (DbParameter p in command.Parameters)
            {
                cmdText = cmdText.Replace(string.Format("@{0}", p.ParameterName), string.Format("'{0}'", p.Value));
                Debug.WriteLine(string.Format("参数名：{0} 参数值：{1}", p.ParameterName, p.Value));
            }
            Debug.WriteLine(cmdText);
            Debug.WriteLine("--------------------------------------------------------------------------------------");
#endif
        }

        #endregion
    }
}
