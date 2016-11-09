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

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using JuCheap.Data.Config;

namespace JuCheap.Data
{
    /// <summary>
    /// JuCheapContext
    /// </summary>
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class JuCheapContext : DbContext
    {
        /// <summary>
        /// JuCheapContext
        /// </summary>
        public JuCheapContext() : base("jucheap")
        {
            //SQL语句拦截器
            //System.Data.Entity.Infrastructure.Interception.DbInterception.Add(new SqlCommandInterceptor());
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串名称</param>
        public JuCheapContext(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除关系
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //移除表名复数形式
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            //配置实体和数据表的关系
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new MenuConfig());
            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new RoleMenuConfig());
            modelBuilder.Configurations.Add(new UserRoleConfig());
            modelBuilder.Configurations.Add(new LoginLogConfig());
            modelBuilder.Configurations.Add(new PageViewConfig());
            modelBuilder.Configurations.Add(new EmailPoolConfig());
            modelBuilder.Configurations.Add(new EmailReceiverConfig());
        }
        ///// <summary>
        ///// 重写SaveChanges方法，增加ID自动生成
        ///// </summary>
        ///// <returns></returns>
        //public override int SaveChanges()
        //{
        //    RegisterIdGenerator<UserEntity>();
        //    RegisterIdGenerator<MenuEntity>();
        //    RegisterIdGenerator<RoleEntity>();
        //    RegisterIdGenerator<RoleMenuEntity>();
        //    RegisterIdGenerator<UserRoleEntity>();
        //    RegisterIdGenerator<LoginLogEntity>();
        //    RegisterIdGenerator<PageViewEntity>();
        //    RegisterIdGenerator<EmailPoolEntity>();
        //    RegisterIdGenerator<EmailReceiverEntity>();
           
        //    return base.SaveChanges();
        //}

        ///// <summary>
        ///// 新增的时候，自动生成ID
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        //private void RegisterIdGenerator<T>() where T : BaseEntity
        //{
        //    foreach (var entity in ChangeTracker.Entries<T>()
        //        .Where(et => et.State == EntityState.Added))
        //    {
        //        entity.Entity.Id = GuidGeneratorHelper.NewGuid().ToString().Replace("-", string.Empty).ToUpper();
        //    }
        //}
    }
}
