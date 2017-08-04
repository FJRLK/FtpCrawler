using Autofac.Integration.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FtpCrawler.Web
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            Services.ServiceManager.OnDependancyRegister += (o, e) =>
            {
                //Inject properties into filter attributes
                e.Builder.RegisterFilterProvider();

                System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("FtpCrawler")).ToArray();

                e.Builder.RegisterControllers(assemblies);
            };

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Services.ServiceManager.Container));
        }
    }
}