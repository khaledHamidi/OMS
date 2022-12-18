using Autofac;
using OMS.Core.Repositories;
using OMS.Core.Services;
using OMS.Core.UnitOfWorks;
using OMS.Data;
using OMS.Data.Repositories;
using OMS.Data.UnitOfWorks;
using OMS.Service.Mapping.Users;
using OMS.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace OMS.API.Modules
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(DatabaseContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(UserProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
