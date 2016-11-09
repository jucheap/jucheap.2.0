using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using JuCheap.Core;
using JuCheap.Web.Jobs;
using Mehdime.Entity;
using Quartz;

namespace JuCheap.Web
{
    /// <summary>
    /// AutoFac属性IOC初始化
    /// </summary>
    public class IocInitializeProperties
    {
        /// <summary>
        /// 获取或设置 Autofac组合IContainer
        /// </summary>
        protected IContainer Container { get; set; }

        public IocInitializeProperties()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Container = builder.Build();
        }

        /// <summary>
        /// 依赖注入初始化
        /// </summary>
        public void Initialize()
        {
            Type baseType = typeof(IDependency);
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().ToArray();//获取已加载到此应用程序域的执行上下文中的程序集。
            Type[] dependencyTypes = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p != baseType).ToArray();//得到接口和实现类
            RegisterDependencyTypes(dependencyTypes);//第一步：注册类型
            SetResolver(assemblies);//第二步：
        }

        /// <summary>
        /// 实现依赖注入接口<see cref="IDependency"/>实现类型的注册
        /// </summary>
        /// <param name="types">要注册的类型集合</param>
        protected void RegisterDependencyTypes(Type[] types)
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<DbContextScopeFactory>()
                .As<IDbContextScopeFactory>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.RegisterTypes(types)
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();//PropertiesAutowired注册为属性注入类型，所有实现IDependency的注册为InstancePerLifetimeScope生命周期
            

            builder.RegisterModule(new QuartzAutofacFactoryModule());
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(EmailJob).Assembly));
            builder.RegisterType<EmailJob>().As<IJob>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.Update(Container);
        }

        /// <summary>
        /// 设置MVC的DependencyResolver注册点
        /// </summary>
        /// <param name="assemblies"></param>
        protected void SetResolver(Assembly[] assemblies)
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(assemblies)
                .AsSelf()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
            builder.Update(Container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
    }
}