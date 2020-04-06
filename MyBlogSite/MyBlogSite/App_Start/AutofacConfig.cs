using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MyBlogSite.Core;

namespace MyBlogSite
{
    public class AutofacConfig
    {
        /// <summary>
        /// Autofac依赖注入
        /// </summary>
        public static void AutofacRegister()
        {
            var builder = new ContainerBuilder();
            //注册MVC控制器（注册所有到控制器，控制器注入，就是需要在控制器的构造函数中接收对象）
            //PropertiesAutowired()：允许属性注入
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
 
            //一次性注册所有实现了IDependency接口的类
            var baseType = typeof(IDependency);
            var assemblies =
                Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf().AsImplementedInterfaces()
                .PropertiesAutowired().InstancePerLifetimeScope();
 
            //设置依赖解析器
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}