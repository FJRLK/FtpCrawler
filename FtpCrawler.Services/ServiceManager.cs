using Autofac;

namespace FtpCrawler.Services
{
    public class ServiceManager
    {
        protected ServiceManager()
        {
        }

        private static Autofac.IContainer RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register<Data.IDatabaseContext>(c => new Data.DatabaseContext(new System.Data.Entity.CreateDatabaseIfNotExists<Data.DatabaseContext>())).SingleInstance();

            builder.RegisterGeneric(typeof(Data.DataRepository<>)).As(typeof(Data.IDataRepository<>)).SingleInstance();

            builder.RegisterType<FtpFileService>().As<Interfaces.IFtpFileService>().SingleInstance();
            builder.RegisterType<FtpFolderService>().As<Interfaces.IFtpFolderService>().SingleInstance();
            builder.RegisterType<FtpServerService>().As<Interfaces.IFtpServerService>().SingleInstance();

            Autofac.IContainer container = builder.Build();
            return container;
        }

        private static Autofac.IContainer _container = null;
        private static Autofac.IContainer Container => _container ?? (_container = RegisterDependencies());

        public static T ResolveService<T>()
        {
            return Container.Resolve<T>();
        }
    }
}